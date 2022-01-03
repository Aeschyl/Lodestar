using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace FBLACodingAndProgramming2021_2022.Geocoding
{
    using global::Geocoding;
    using global::Geocoding.Microsoft;
    using Newtonsoft.Json;
    using System.Device.Location;
    using System.Globalization;
    using System.Net.Sockets;

    class Geocoder
    {
        public class IpInfo
        {
            [JsonProperty("ip")]
            public string Ip { get; set; }

            [JsonProperty("hostname")]
            public string Hostname { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("region")]
            public string Region { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("loc")]
            public string Loc { get; set; }

            [JsonProperty("org")]
            public string Org { get; set; }

            [JsonProperty("postal")]
            public string Postal { get; set; }
        }
        public static async Task<string> GetCoordinatesAsync(string adress) 
        {
            IGeocoder geocoder = new BingMapsGeocoder("qc9Y7SHHRgYbev4fUy0q~ZgF6eo0fD9ieP3VnKoDX_Q~AnM4TYqGE82d-jah6trRCttSyWK53fdPmYnyOjGbcYfmD61QQYzwoRH2oNJN9AZG");
            IEnumerable<Address> addresses = geocoder.Geocode(adress);

            return addresses.First().Coordinates.Latitude + ", " + addresses.First().Coordinates.Longitude;
        }

        public static string GetCoordinatesFromLocationSensor()
        {
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

            // Do not suppress prompt, and wait 1000 milliseconds to start.
            watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));

            GeoCoordinate coord = watcher.Position.Location;

            if (coord.IsUnknown != true)
            {
                return String.Format("{0}, {1}", coord.Latitude, coord.Longitude);
            }
            throw new Exception(string.Format("Error: {0}, {1}", coord.Latitude, coord.Longitude));
            
        }

        public static string GetCoordinatesFromIpAddress()
        {
            string ipAddress;
            //Get Ip Address
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");

            //Get and return Location
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ipAddress);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.City);
                ipInfo.City = myRI1.EnglishName;
            }
            catch (Exception)
            {
                ipInfo.City = null;
            }

            return ipInfo.City;
        }
        
    }
}
