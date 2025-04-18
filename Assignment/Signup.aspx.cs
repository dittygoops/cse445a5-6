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
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCaptcha();
            }
        }

        private void LoadCaptcha()
        {
            var captchaService = new CaptchaService();
            CaptchaImage.ImageUrl = captchaService.GenerateCaptcha();
        }

        protected void RefreshCaptcha_Click(object sender, EventArgs e)
        {
            LoadCaptcha();
        }

        protected void SignupButton_Click(object sender, EventArgs e)
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

            string enteredPass = Hasher.Hash(PasswordInput.Text.Trim());
            string enteredConfirmPass = Hasher.Hash(ConfirmPasswordInput.Text.Trim());

            if (enteredPass != enteredConfirmPass)
            {
                CaptchaFeedback.Text = "❌ Passwords do not match.";
                CaptchaFeedback.ForeColor = System.Drawing.Color.Red;
                LoadCaptcha();
                return;
            }

            string filePath = Server.MapPath("~/Member.xml");

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            // check if username already exists
            XmlNode existingUser = doc.SelectSingleNode("//Member[Username='" + UsernameInput.Text.Trim() + "']");
            if (existingUser != null)
            {
                CaptchaFeedback.Text = "❌ Username already exists.";
                CaptchaFeedback.ForeColor = System.Drawing.Color.Red;
                LoadCaptcha();
                return;
            }
            // write to xml file
            XmlNode newUser = doc.CreateNode(XmlNodeType.Element, "Member", "");
            newUser.AppendChild(doc.CreateElement("Username")).InnerText = UsernameInput.Text.Trim();
            newUser.AppendChild(doc.CreateElement("Password")).InnerText = enteredPass;
            doc.DocumentElement.AppendChild(newUser);
            doc.Save(filePath);

            Session["user"] = UsernameInput.Text.Trim();

            SignupStatusLabel.Text = "✅ Signup successful!";
            SignupStatusLabel.ForeColor = System.Drawing.Color.Green;

            // Redirect to correct page with parameter
            Response.Redirect("Login.aspx?type=member");
        }
    }
}