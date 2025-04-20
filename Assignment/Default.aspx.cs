using System;
using System.Web;
using System.Web.UI;

namespace Assignment
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie cookie = Request.Cookies["UserAuth"];
                if (cookie != null)
                {
                    string role = cookie["Role"];
                    string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath).ToLower();

                    if (role == "member" && currentPage != "member.aspx")
                        Response.Redirect("~/Member.aspx", false);

                    else if (role == "staff" && currentPage != "staff.aspx")
                        Response.Redirect("~/Staff.aspx", false);
                }
            }
        }
    }
}
