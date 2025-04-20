using System;
using LoginSecurityLib;
using System.Diagnostics;
namespace Assignment
{
    public partial class CaptchaTryIt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCaptcha();
        }

        private void LoadCaptcha()
        {
            var captchaService = new CaptchaService();
            var captcha = captchaService.GenerateCaptcha();
            CaptchaImage.ImageUrl = captcha;
            CaptchaEncodedText.Text = captcha;
        }

        protected void RefreshCaptchaButton_Click(object sender, EventArgs e)
        {
            LoadCaptcha();
        }

        protected void VerifyCaptchaButton_Click(object sender, EventArgs e)
        {
            var captchaService = new CaptchaService();
            bool isValid = captchaService.VerifyCaptcha(CaptchaInput.Text.Trim());

            if (!isValid)
            {
                CaptchaOutput.Text = "❌ Incorrect captcha, please try again.";
                CaptchaOutput.ForeColor = System.Drawing.Color.Red;
                LoadCaptcha();
                return;
            }

            CaptchaOutput.Text = "✅ Captcha verified successfully.";
            CaptchaOutput.ForeColor = System.Drawing.Color.Green;
        }
    }
}
