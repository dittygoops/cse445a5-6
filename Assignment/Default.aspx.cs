using System;
using System.Web;
using System.Web.UI;

namespace Assignment
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("Directory.aspx", false);
        }
    }
}
