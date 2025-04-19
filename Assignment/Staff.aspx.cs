using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml;

namespace Assignment
{
    public partial class Staff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEvents();
            }
        }

        private void LoadEvents()
        {
            string filePath = Server.MapPath("~/Events.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            var events = doc.SelectNodes("//Event").Cast<XmlNode>().Select(e => new
            {
                Name = e["Name"]?.InnerText,
                Info = e["Info"]?.InnerText,
                Date = e["Date"]?.InnerText,
                Time = e["Time"]?.InnerText,
                Location = e["Location"]?.InnerText,
                Hours = e["Hours"]?.InnerText,
                RSVPList = e["RSVPed"] != null ? string.Join(", ", e["RSVPed"].SelectNodes("User").Cast<XmlNode>().Select(u => u.InnerText)) : "None"
            }).ToList();

            StaffRepeater.DataSource = events;
            StaffRepeater.DataBind();
        }


        protected void StaffRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                string eventName = e.CommandArgument.ToString();
                string filePath = Server.MapPath("~/Events.xml");

                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                XmlNode eventNode = doc.SelectSingleNode($"//Event[Name='{eventName}']");
                if (eventNode != null)
                {
                    // Get new values from TextBoxes
                    string newDate = ((TextBox)e.Item.FindControl("DateTextBox")).Text.Trim();
                    string newTime = ((TextBox)e.Item.FindControl("TimeTextBox")).Text.Trim();
                    string newLocation = ((TextBox)e.Item.FindControl("LocationTextBox")).Text.Trim();
                    string newHours = ((TextBox)e.Item.FindControl("HoursTextBox")).Text.Trim();
                    string newInfo = ((TextBox)e.Item.FindControl("InfoTextBox")).Text.Trim();

                    // Track which fields changed
                    List<string> changes = new List<string>();

                    if (eventNode["Date"].InnerText != newDate)
                    {
                        eventNode["Date"].InnerText = newDate;
                        changes.Add($"Date changed to {newDate}");
                    }

                    if (eventNode["Time"].InnerText != newTime)
                    {
                        eventNode["Time"].InnerText = newTime;
                        changes.Add($"Time changed to {newTime}");
                    }

                    if (eventNode["Location"].InnerText != newLocation)
                    {
                        eventNode["Location"].InnerText = newLocation;
                        changes.Add($"Location changed to {newLocation}");
                    }

                    if (eventNode["Hours"].InnerText != newHours)
                    {
                        eventNode["Hours"].InnerText = newHours;
                        changes.Add($"Hours changed to {newHours}");
                    }

                    if (eventNode["Info"].InnerText != newInfo)
                    {
                        eventNode["Info"].InnerText = newInfo;
                        changes.Add("Info updated");
                    }

                    doc.Save(filePath);

                    if (changes.Count > 0)
                    {
                        ShowModal("✅ Event updated: " + string.Join(", ", changes));
                    }
                    else
                    {
                        ShowModal("No changes were made to this event.");
                    }

                    LoadEvents();
                }
            }
        }
        private void ShowModal(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showModalScript", $"<script>showModal('{message}');</script>");
        }


    }
}
