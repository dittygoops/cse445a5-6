﻿using System;
using System.Linq;
using System.Web;
using System.Web.UI;
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
                HttpCookie cookie = Request.Cookies["UserAuth"];
                if (cookie != null)
                {
                    string role = cookie["Role"];
                    if (role == "member")
                        Response.Redirect("~/Member.aspx");
                    else if (role == "staff")
                        Response.Redirect("~/Staff.aspx");
                }

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

            string role = Request.QueryString["type"];
            if (string.IsNullOrEmpty(role))
            {
                CaptchaFeedback.Text = "❌ Login type missing. Use ?type=member or ?type=staff.";
                CaptchaFeedback.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string tag = char.ToUpper(role[0]) + role.Substring(1);
            string enteredPass = Hasher.Hash(PasswordInput.Text.Trim());
            string filePath = Server.MapPath("~/Member.xml");

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            bool match = doc.SelectNodes("//" + tag).Cast<XmlNode>().Any(user =>
                user["Username"]?.InnerText == UsernameInput.Text.Trim() &&
                user["Password"]?.InnerText == enteredPass
            );

            if (match)
            {
                HttpCookie userCookie = new HttpCookie("UserAuth");
                userCookie["Username"] = UsernameInput.Text.Trim();
                userCookie["Role"] = role;
                userCookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(userCookie);

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
