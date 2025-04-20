using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment
{
    public partial class Directory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load the service directory data
                LoadServiceDirectory();
            }
        }

        private void LoadServiceDirectory()
        {
            // TODO: Implement the logic to load the service directory data
        }
    }
}