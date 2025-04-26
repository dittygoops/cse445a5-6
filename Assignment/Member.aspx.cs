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
                    Response.Redirect("~/Default.aspx", false);
                    return;
                }

                if (Session["RSVPMessage"] != null)
                {
                    string message = Session["RSVPMessage"].ToString();
                    Session.Remove("RSVPMessage");

                    ClientScript.RegisterStartupScript(this.GetType(), "delayedModal", $@"
                        <script>
                            window.addEventListener('load', function () {{
                                showModal('{message}');
                                setTimeout(() => {{
                                    const modal = bootstrap.Modal.getInstance(document.getElementById('FeedbackModal'));
                                    if (modal) modal.hide();
                                }}, 3000);
                            }});
                        </script>");
                }

                LoadEvents();
            }

            if (Request["__EVENTTARGET"] == "CheckInWithLocation")
            {
                string[] args = Request["__EVENTARGUMENT"].Split('|');
                string eventName = args[0];
                double userLat = double.Parse(args[1]);
                double userLon = double.Parse(args[2]);
                HandleProximityCheck(eventName, userLat, userLon);
            }
        }

        private void HandleProximityCheck(string eventName, double userLat, double userLon)
        {
            string filePath = Server.MapPath("~/Events.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode eventNode = doc.SelectSingleNode($"//Event[Name='{eventName}']");
            if (eventNode == null)
            {
                ShowModal("❌ Event not found.");
                return;
            }

            double eventLat = double.Parse(eventNode["Latitude"].InnerText);
            double eventLon = double.Parse(eventNode["Longitude"].InnerText);

            double distance = CalculateDistance(userLat, userLon, eventLat, eventLon);

            System.Diagnostics.Debug.WriteLine("Distance: " + distance + " miles");

            if (distance > 1)
            {
                ShowModal("❌ You were too far away to RSVP.");
            }
            else
            {
                ShowModal("✅ You checked in successfully.");
                // Add check-in logic here if needed
            }
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 3958.8; // Earth radius in miles
            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            lat1 = DegreesToRadians(lat1);
            lat2 = DegreesToRadians(lat2);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));
            // not yet imported System.Diagnostics
            System.Diagnostics.Debug.WriteLine("Distance: " + R * c + " miles");
            return R * c;
        }

        private double DegreesToRadians(double deg)
        {
            return deg * Math.PI / 180.0;
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

                var attendingUsers = e["Attending"]?.SelectNodes("User")
                    .Cast<XmlNode>()
                    .Select(u => u.InnerText.Split('|')[0]) // get just the username
                    .ToList() ?? new List<string>();


                return new
                {
                    Name = e["Name"]?.InnerText,
                    Info = e["Info"]?.InnerText,
                    Date = e["Date"]?.InnerText,
                    Time = e["Time"]?.InnerText,
                    Location = e["Location"]?.InnerText,
                    Map = e["Map"]?.InnerText,
                    Hours = e["Hours"]?.InnerText,
                    IsRSVPed = rsvpedUsers.Contains(currentUser),
                    IsCheckedIn = attendingUsers.Contains(currentUser)

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

                // Check if the user already RSVPed
                XmlNode existingUser = rsvpedNode.SelectNodes("User")
                    .Cast<XmlNode>()
                    .FirstOrDefault(node => node.InnerText == username);

                if (existingUser == null)
                {
                    // ADD user
                    XmlElement newUser = doc.CreateElement("User");
                    newUser.InnerText = username;
                    rsvpedNode.AppendChild(newUser);

                    Session["RSVPMessage"] = "✅ You have RSVPed for this event.";
                }
                else
                {
                    // REMOVE user
                    rsvpedNode.RemoveChild(existingUser);
                    
                    // if the user is attending, remove them from attending
                    XmlNode attendingNode = eventNode["Attending"];
                    // iterate through the children of the attending node and remove the user
                    foreach (XmlNode child in attendingNode.ChildNodes)
                    {
                        if (child.InnerText.StartsWith(username + "|"))
                        {
                            attendingNode.RemoveChild(child);
                        }
                    }
                    Session["RSVPMessage"] = "❌ You have removed your RSVP from this event.";
                }

                try
                {
                    doc.Save(filePath);
                }
                catch (Exception ex)
                {
                    ShowModal($"⚠️ Error updating RSVP: {ex.Message}");
                    return;
                }

                Response.Redirect(Request.RawUrl, false); // Reload page and trigger message
            }

            if (e.CommandName == "CheckIn")
            {
                XmlNode rsvpedNode = eventNode["RSVPed"];
                bool isRSVPed = rsvpedNode != null &&
                    rsvpedNode.SelectNodes("User")
                    .Cast<XmlNode>()
                    .Any(node => node.InnerText == username);

                if (!isRSVPed)
                {
                    ShowModal("⚠️ You must RSVP before checking in.");
                    return;
                }
                

                XmlNode attendingNode = eventNode["Attending"];
                if (attendingNode == null)
                {
                    attendingNode = doc.CreateElement("Attending");
                    eventNode.AppendChild(attendingNode);
                }

                bool alreadyCheckedIn = false;
                foreach (XmlNode child in attendingNode.ChildNodes)
                {
                    if (child.InnerText.StartsWith(username + "|"))
                    {
                        alreadyCheckedIn = true;
                    }
                }

                if (!alreadyCheckedIn)
                {
                    XmlElement newUser = doc.CreateElement("User");
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    newUser.InnerText = $"{username}|{timestamp}";
                    attendingNode.AppendChild(newUser);

                    try
                    {
                        doc.Save(filePath);
                        Session["RSVPMessage"] = "✅ You have successfully checked into this event.";
                    }
                    catch (Exception ex)
                    {
                        ShowModal($"⚠️ Error during check-in: {ex.Message}");
                        return;
                    }
                }
                else
                {
                    // iterate through the children of the attending node and remove the user
                    foreach (XmlNode child in attendingNode.ChildNodes)
                        {
                        if (child.InnerText.StartsWith(username + "|"))
                        {
                            attendingNode.RemoveChild(child);
                        }
                    }   
                    Session["RSVPMessage"] = "❌ Removed from attending.";
                }

                try
                {
                    doc.Save(filePath);
                }
                catch (Exception ex)
                {
                    ShowModal($"⚠️ Error updating check-in: {ex.Message}");
                    return;
                }

                Response.Redirect(Request.RawUrl, false); // reload with message
            }



            LoadEvents();
        }

        private void ShowModal(string message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showModalScript", $"<script>showModal('{message}');</script>");
        }
    }
}
