using System;
using System.Web;

namespace Assignment
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie cookie = Request.Cookies["UserAuth"];
                string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath).ToLower();

                if (cookie != null && currentPage != "default.aspx")
                {
                    LogoutButton.Visible = true;
                }
                else
                {
                    LogoutButton.Visible = false;
                }
            }
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["UserAuth"] != null)
            {
                HttpCookie cookie = new HttpCookie("UserAuth");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
    }
}