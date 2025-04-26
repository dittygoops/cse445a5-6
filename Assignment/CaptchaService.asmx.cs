using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Services;

namespace Assignment
{
    [WebService(Namespace = "http://assignment.edu/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CaptchaService : WebService
    {
        [WebMethod(EnableSession = true)]
        public string GenerateCaptcha()
        {
            string captchaText = GenerateRandomText(6);

            // Save to session
            Session["captcha"] = captchaText;

            using (Bitmap bmp = new Bitmap(180, 50))
            using (Graphics g = Graphics.FromImage(bmp))
            using (MemoryStream ms = new MemoryStream())
            {
                g.Clear(Color.White);

                // Add background noise
                DrawNoise(g, bmp.Width, bmp.Height);

                using (Font font = new Font("Arial", 28, FontStyle.Bold | FontStyle.Italic))
                {
                    g.DrawString(captchaText, font, Brushes.Black, new PointF(10, 10));
                }

                bmp.Save(ms, ImageFormat.Png);
                return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
            }
        }

        [WebMethod(EnableSession = true)]
        public bool VerifyCaptcha(string input)
        {
            string storedCaptcha = Session["captcha"] as string;
            return string.Equals(input, storedCaptcha, StringComparison.OrdinalIgnoreCase);
        }

        private string GenerateRandomText(int length)
        {
            var chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var rand = new Random();
            char[] buffer = new char[length];
            for (int i = 0; i < length; i++)
                buffer[i] = chars[rand.Next(chars.Length)];
            return new string(buffer);
        }

        private void DrawNoise(Graphics g, int width, int height)
        {
            Random rand = new Random();
            Pen pen = new Pen(Color.Gray);

            // Draw random lines
            for (int i = 0; i < 30; i++)
            {
                int x1 = rand.Next(width);
                int y1 = rand.Next(height);
                int x2 = rand.Next(width);
                int y2 = rand.Next(height);
                g.DrawLine(pen, x1, y1, x2, y2);
            }
        }
    }
}
