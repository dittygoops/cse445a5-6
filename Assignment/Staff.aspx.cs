using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

namespace Assignment
{
    public partial class Staff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["UserAuth"];
            if (cookie == null || cookie["Role"] != "staff")
            {
                Response.Redirect("~/Default.aspx", false);
                return;
            }

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
                RSVPList = e["RSVPed"] != null ? string.Join(", ", e["RSVPed"].SelectNodes("User").Cast<XmlNode>().Select(u => u.InnerText.Split('|')[0])) : "None",
                AttendingList = e["Attending"] != null ? string.Join("", e["Attending"].SelectNodes("User").Cast<XmlNode>().Select(u =>
                {
                    var parts = u.InnerText.Split('|');
                    string name = parts[0];
                    string date = "N/A", time = "N/A";
                    if (parts.Length > 1 && DateTime.TryParse(parts[1], out DateTime dt))
                    {
                        date = dt.ToString("yyyy-MM-dd");
                        time = dt.ToString("HH:mm:ss");
                    }
                    return $"<tr><td>{name}</td><td>{date}</td><td>{time}</td></tr>";
                })) : "<tr><td colspan='3'>None</td></tr>"
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
                    string newDate = ((TextBox)e.Item.FindControl("DateTextBox")).Text.Trim();
                    string newTime = ((TextBox)e.Item.FindControl("TimeTextBox")).Text.Trim();
                    string newLocation = ((TextBox)e.Item.FindControl("LocationTextBox")).Text.Trim();
                    string newHours = ((TextBox)e.Item.FindControl("HoursTextBox")).Text.Trim();
                    string newInfo = ((TextBox)e.Item.FindControl("InfoTextBox")).Text.Trim();

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

                        var geoService = new GeoService.GeoLocationWebServiceSoapClient();
                        var result = geoService.GetLatLong(newLocation);
                        if (result.Success)
                        {
                            SetOrUpdateXmlNode(doc, eventNode, "Latitude", result.Latitude.ToString());
                            SetOrUpdateXmlNode(doc, eventNode, "Longitude", result.Longitude.ToString());
                            changes.Add("Lat/Long updated");
                        }
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

                    ShowModal(changes.Count > 0 ? $"✅ Event updated: {string.Join(", ", changes)}" : "No changes were made to this event.");
                    LoadEvents();
                }
            }
        }

        protected void CreateEventButton_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("~/Events.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode root = doc.SelectSingleNode("/Events");
            XmlElement newEvent = doc.CreateElement("Event");

            string address = NewEventLocation.Text.Trim();
            var geoService = new GeoService.GeoLocationWebServiceSoapClient();
            var coords = geoService.GetLatLong(address);

            newEvent.AppendChild(CreateElement(doc, "Name", NewEventName.Text.Trim()));
            newEvent.AppendChild(CreateElement(doc, "Info", NewEventInfo.Text.Trim()));
            newEvent.AppendChild(CreateElement(doc, "Date", NewEventDate.Text.Trim()));
            newEvent.AppendChild(CreateElement(doc, "Time", NewEventTime.Text.Trim()));
            newEvent.AppendChild(CreateElement(doc, "Location", address));
            newEvent.AppendChild(CreateElement(doc, "Hours", NewEventHours.Text.Trim()));

            newEvent.AppendChild(doc.CreateElement("RSVPed"));
            newEvent.AppendChild(doc.CreateElement("Attending"));

            if (coords != null && coords.Success)
            {
                newEvent.AppendChild(CreateElement(doc, "Latitude", coords.Latitude.ToString()));
                newEvent.AppendChild(CreateElement(doc, "Longitude", coords.Longitude.ToString()));
            }

            root.AppendChild(newEvent);
            doc.Save(filePath);

            ShowModal("✅ New event added successfully!");
            LoadEvents();
            ClearFormFields();
        }

        private XmlElement CreateElement(XmlDocument doc, string name, string value)
        {
            XmlElement element = doc.CreateElement(name);
            element.InnerText = value;
            return element;
        }

        private void SetOrUpdateXmlNode(XmlDocument doc, XmlNode parent, string nodeName, string value)
        {
            XmlNode node = parent[nodeName];
            if (node == null)
            {
                node = doc.CreateElement(nodeName);
                parent.AppendChild(node);
            }
            node.InnerText = value;
        }

        private void ClearFormFields()
        {
            NewEventName.Text = "";
            NewEventDate.Text = "";
            NewEventTime.Text = "";
            NewEventLocation.Text = "";
            NewEventHours.Text = "";
            NewEventInfo.Text = "";
        }

        private void ShowModal(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showModalScript", $"<script>showModal('{message}');</script>");
        }
    }
}
