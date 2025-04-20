using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Assignment
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Session_Start(object sender, EventArgs e)
        {
            string sessionId = Session.SessionID;

            HttpCookie cookie = HttpContext.Current.Request.Cookies["UserAuth"];
            string user = cookie?["Username"] ?? "Unknown";
            string role = cookie?["Role"] ?? "Unknown";

            string message = $"✅ Session {sessionId} started: {user} ({role}) at {DateTime.Now}";

            // Debug output
            System.Diagnostics.Debug.WriteLine(message);

            // Log to file
            try
            {
                string path = Server.MapPath("~/App_Data/sessionlog.txt");

                // Ensure the App_Data directory exists
                string dir = System.IO.Path.GetDirectoryName(path);
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }

                System.IO.File.AppendAllText(path, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("❌ Failed to write session log: " + ex.Message);
            }
        }
    }
}