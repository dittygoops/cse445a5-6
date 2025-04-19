using Newtonsoft.Json.Linq;
using System.Net;
using System.Web.Services;
using System.Web;
using System;

namespace GeoLocationService
{
    [WebService(Namespace = "http://assignment.edu/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GeoLocationWebService : System.Web.Services.WebService
    {
        private const string ApiKey = "1f8559fa72c14b7d88b0292fcb580f7b";

        [WebMethod]
        public LocationResult GetLatLong(string address)
        {
            try
            {
                string url = $"https://api.geoapify.com/v1/geocode/search?text={HttpUtility.UrlEncode(address)}&format=json&apiKey={ApiKey}";

                using (WebClient client = new WebClient())
                {
                    string response = client.DownloadString(url);
                    JObject json = JObject.Parse(response);

                    if (json["results"] != null && json["results"].HasValues)
                    {
                        var result = json["results"][0];
                        return new LocationResult
                        {
                            Latitude = result.Value<double>("lat"),
                            Longitude = result.Value<double>("lon"),
                            Success = true,
                            Message = "Success"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new LocationResult { Success = false, Message = ex.Message };
            }

            return new LocationResult { Success = false, Message = "No results found" };
        }
    }

    public class LocationResult
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
