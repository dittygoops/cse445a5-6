using System;
using LoginSecurityLib;

namespace Assignment
{
    public partial class HashTryIt : System.Web.UI.Page
    {
        protected void HashButton_Click(object sender, EventArgs e)
        {
            string input = InputToHash.Text.Trim();

            if (!string.IsNullOrEmpty(input))
            {
                string hash = Hasher.Hash(input);
                HashOutput.Text = $"<strong>Hashed Output:</strong> <code>{hash}</code>";
            }
            else
            {
                HashOutput.Text = "<span style='color:red;'>Please enter a value to hash.</span>";
            }
        }
    }
}
