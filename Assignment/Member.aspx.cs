using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

namespace Assignment
{
    public partial class Member : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie cookie = Request.Cookies["UserAuth"];
                if (cookie == null || cookie["Role"] != "member")
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                LoadEvents();
            }
        }

        private void LoadEvents()
        {
            string filePath = Server.MapPath("~/Events.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            HttpCookie userCookie = Request.Cookies["UserAuth"];
            string currentUser = userCookie != null ? userCookie["Username"] : "";

            var events = doc.SelectNodes("//Event").Cast<XmlNode>().Select(e =>
            {
                var rsvpedUsers = e["RSVPed"]?.SelectNodes("User")
                    .Cast<XmlNode>()
                    .Select(u => u.InnerText)
                    .ToList() ?? new List<string>();

                return new
                {
                    Name = e["Name"]?.InnerText,
                    Info = e["Info"]?.InnerText,
                    Date = e["Date"]?.InnerText,
                    Time = e["Time"]?.InnerText,
                    Location = e["Location"]?.InnerText,
                    Hours = e["Hours"]?.InnerText,
                    IsRSVPed = rsvpedUsers.Contains(currentUser)
                };
            }).ToList();

            EventRepeater.DataSource = events;
            EventRepeater.DataBind();
        }

        protected void EventRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string eventName = e.CommandArgument.ToString();
            string filePath = Server.MapPath("~/Events.xml");
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(filePath);
            }
            catch (Exception ex)
            {
                ShowModal($"⚠️ Error loading events file: {ex.Message}");
                return;
            }

            XmlNode eventNode = doc.SelectSingleNode($"//Event[Name='{eventName}']");
            if (eventNode == null)
            {
                ShowModal("⚠️ Event not found.");
                return;
            }

            HttpCookie userCookie = Request.Cookies["UserAuth"];
            if (userCookie == null || string.IsNullOrEmpty(userCookie["Username"]))
            {
                ShowModal("⚠️ You must be logged in to RSVP.");
                return;
            }

            string username = userCookie["Username"];

            if (e.CommandName == "RSVP")
            {
                XmlNode rsvpedNode = eventNode["RSVPed"];
                if (rsvpedNode == null)
                {
                    rsvpedNode = doc.CreateElement("RSVPed");
                    eventNode.AppendChild(rsvpedNode);
                }

                bool alreadyRSVPed = rsvpedNode.SelectNodes("User")
                    .Cast<XmlNode>()
                    .Any(node => node.InnerText == username);

                if (!alreadyRSVPed)
                {
                    XmlElement newUser = doc.CreateElement("User");
                    newUser.InnerText = username;
                    rsvpedNode.AppendChild(newUser);

                    try
                    {
                        doc.Save(filePath);
                        ShowModal("✅ You have RSVPed for this event.");
                    }
                    catch (Exception ex)
                    {
                        ShowModal($"⚠️ Error saving RSVP: {ex.Message}");
                    }
                }
                else
                {
                    ShowModal("❌ You have already RSVPed to this event.");
                }
            }

            LoadEvents();
        }

        private void ShowModal(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showModalScript", $"<script>showModal('{message}');</script>");
        }
    }
}
