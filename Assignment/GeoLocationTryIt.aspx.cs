using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment
{
    public partial class GeoLocationTryIt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void GetLocationButton_Click(object sender, EventArgs e)
        {
            string address = AddressInput.Text;
            if (string.IsNullOrEmpty(address))
            {
                LocationResult.Text = "Please enter an address.";
                return;
            }

            // Call the GeoLocationWebService
            var geoService = new GeoService.GeoLocationWebServiceSoapClient();
            var coords = geoService.GetLatLong(address);

            if (coords != null)
            {   
                LocationResult.Text = $"Latitude: {coords.Latitude}, Longitude: {coords.Longitude}";
            }
            else
            {
                LocationResult.Text = "Failed to get location.";
            }
        }
    }
}