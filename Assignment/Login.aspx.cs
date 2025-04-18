using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using LoginSecurityLib;

namespace Assignment
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCaptcha();

                string userType = Request.QueryString["type"];
                if (userType == "staff")
                    loginTitle.InnerText = "Staff Login";
                else
                    loginTitle.InnerText = "Member Login";
            }
        }

        private void LoadCaptcha()
        {
            var captchaService = new CaptchaService();
            CaptchaImage.ImageUrl = captchaService.GenerateCaptcha();
            CaptchaFeedback.Text = "";
        }

        protected void RefreshCaptcha_Click(object sender, EventArgs e)
        {
            LoadCaptcha();
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            var captchaService = new CaptchaService();
            bool isValid = captchaService.VerifyCaptcha(CaptchaInput.Text.Trim());

            if (!isValid)
            {
                CaptchaFeedback.Text = "❌ Incorrect captcha, please try again.";
                CaptchaFeedback.ForeColor = System.Drawing.Color.Red;
                LoadCaptcha();
                return;
            }

            // Confirm Captcha passed
            CaptchaFeedback.Text = "✅ Captcha verified.";
            CaptchaFeedback.ForeColor = System.Drawing.Color.Green;

            // Get role (member or staff)
            string role = Request.QueryString["type"];

            if (string.IsNullOrEmpty(role))
            {
                CaptchaFeedback.Text = "❌ Login type missing. Use ?type=member or ?type=staff.";
                CaptchaFeedback.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string tag = char.ToUpper(role[0]) + role.Substring(1); // "Member" or "Staff"

            string enteredUser = Hasher.Hash(UsernameInput.Text.Trim());
            string enteredPass = Hasher.Hash(PasswordInput.Text.Trim());

            string filePath = Server.MapPath("~/Member.xml");

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            bool match = doc.SelectNodes("//" + tag).Cast<XmlNode>().Any(user =>
                user["Username"]?.InnerText == enteredUser &&
                user["Password"]?.InnerText == enteredPass
            );

            if (match)
            {
                Session["user"] = UsernameInput.Text.Trim();
                Session["role"] = role;

                LoginStatusLabel.Text = "✅ Login successful!";
                LoginStatusLabel.ForeColor = System.Drawing.Color.Green;

                // Redirect to correct page
                Response.Redirect(role == "staff" ? "Staff.aspx" : "Member.aspx");
            }
            else
            {
                CaptchaFeedback.Text = "❌ Invalid username or password.";
                CaptchaFeedback.ForeColor = System.Drawing.Color.Red;
                LoadCaptcha();
            }
        }






    }
}