using Newtonsoft.Json.Linq;
using System.Net;
using System.Web.Services;
using System.Web;
using System;
using System.Globalization;

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

        [WebMethod]
        public MapResult GetMapImageUrl(double latitude, double longitude)
        {
            try
            {
                // --- Basic Input Validation (Optional but recommended) ---
                if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
                {
                    return new MapResult { Success = false, Message = "Invalid latitude or longitude values." };
                }

                // --- Define Map Parameters ---
                int width = 600;        // Image width in pixels
                int height = 400;       // Image height in pixels
                int zoom = 15;          // Zoom level (1-20)
                string markerColor = "ff0000"; // Red marker color (hex without #)
                string markerSize = "medium"; // Marker size

                // Use InvariantCulture to ensure '.' is used as the decimal separator
                string centerParam = $"lonlat:{longitude.ToString(CultureInfo.InvariantCulture)},{latitude.ToString(CultureInfo.InvariantCulture)}";

                // Note: Geoapify uses lon,lat order for markers as well. 
                // Color '#' needs to be URL encoded as '%23'
                string markerParam = $"lonlat:{longitude.ToString(CultureInfo.InvariantCulture)},{latitude.ToString(CultureInfo.InvariantCulture)};color:%23{markerColor};size:{markerSize}";

                // --- Construct the Static Map URL ---
                string mapUrl = $"https://maps.geoapify.com/v1/staticmap?" +
                                $"style=osm-carto" +          // Choose a map style (e.g., osm-carto, osm-bright, klokantech-basic)
                                $"&width={width}" +
                                $"&height={height}" +
                                $"&center={centerParam}" +
                                $"&zoom={zoom}" +
                                $"&marker={markerParam}" +
                                $"&apiKey={ApiKey}";

                return new MapResult
                {
                    MapUrl = mapUrl,
                    Success = true,
                    Message = "Map URL generated successfully."
                };
            }
            catch (Exception ex)
            {
                // Log the exception
                return new MapResult { Success = false, Message = $"An error occurred: {ex.Message}" };
            }
        }
    }
}

    public class LocationResult
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class MapResult
    {
        public string MapUrl { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
