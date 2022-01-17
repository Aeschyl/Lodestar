/*
    A program that contains the various methods including manual address geocoding, location sensor geocoding, and IP address geocoding
*/

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
    using System.IO;
    using System.Net.Sockets;
    using System.Web;

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

<<<<<<< HEAD
        // Manual Address Geocoding where the user can enter their address for the location option
        public static List<string> GetCoordinatesAsync(string address) 
=======
        public static List<string> GetCoordinatesAsync(string address)

>>>>>>> b21452925ef52a48cbcc3038c56b890da1a6db20
        {
            string result;

            // Connects to our own server so that api keys are safe
            var requestsServerUrl = new StringBuilder(@"https://touristserver.sami200.repl.co/bingmaps?");

            var requestUrl = new StringBuilder(@"https://dev.virtualearth.net/REST/v1/Locations?q=");

            requestsServerUrl.Append(@"url=");

<<<<<<< HEAD
            requestUrl.Append(address);

            requestsServerUrl.Append(HttpUtility.UrlEncode(requestUrl.ToString()));  // Encoding the url to pass it as a parameter to our server
=======


            requestUrl.Append(address);


            requestsServerUrl.Append(HttpUtility.UrlEncode(requestUrl.ToString()));
>>>>>>> b21452925ef52a48cbcc3038c56b890da1a6db20

            var request = (HttpWebRequest)WebRequest.Create(requestsServerUrl.ToString());

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }


            var addresses = JsonConvert.DeserializeObject<List<double>>(result);
            var addressesString = new List<string>();
            addressesString.Add(addresses.First().ToString());
            addressesString.Add(addresses.Last().ToString());


            return addressesString;
        }

        // Uses the user's location sensor to procure accurate location data for the user
        public static List<string> GetCoordinatesFromLocationSensor()
        {
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

            // Do not suppress prompt, and wait 1000 milliseconds to start.
            watcher.TryStart(true, TimeSpan.FromMilliseconds(5000));

            GeoCoordinate coord = watcher.Position.Location;

            if (coord.IsUnknown != true)
            {
                var list = new List<string>();
                list.Add(coord.Latitude.ToString());
                list.Add(coord.Longitude.ToString());
                return list;
            }
            throw new Exception(string.Format("Error: {0}, {1}", coord.Latitude, coord.Longitude));

        }

<<<<<<< HEAD
        // This method find the IP Address for the user
=======

>>>>>>> b21452925ef52a48cbcc3038c56b890da1a6db20
        private static string GetIPAddressAsync()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            request.Proxy = null;

            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }

        // This method calls our server for a request to an API that returns the coordinates of an IP A
        public static List<string> GetCoordinatesFromIpAddress()
        {
<<<<<<< HEAD
            string ipAddress = GetIPAddressAsync(); //Get IP Address
=======
            string ipAddress;
            try
            {
                ipAddress = GetIPAddressAsync();
                //Get Ip Address
            }
            catch (Exception)
            {
                return null;
            }

>>>>>>> b21452925ef52a48cbcc3038c56b890da1a6db20

            string jsonString;

            StringBuilder url = new StringBuilder(@"https://ipinfo.io/");
            url.Append(ipAddress);
            url.Append("/json");
            //Get and return Location
            var request = (HttpWebRequest)WebRequest.Create(url.ToString());

            /*using (HttpWebResponse response =  await  Task.Factory.FromAsync(
            request.BeginGetResponse,
            asyncResult => request.EndGetResponse(asyncResult),
            (object)null) as HttpWebResponse)
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = await reader.ReadToEndAsync();
            }*/

<<<<<<< HEAD
            Task<WebResponse> task = Task.Factory.FromAsync(
            request.BeginGetResponse,
            asyncResult => request.EndGetResponse(asyncResult),
            (object)null);
=======
>>>>>>> b21452925ef52a48cbcc3038c56b890da1a6db20
            
            try
            {
                using (WebResponse response = request.GetResponse())
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    jsonString = stream.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return null;
            }





            

            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            var list = new List<string>();
            var tempList = values["loc"].Split(',');
            list.Add(tempList[1]);
            list.Add(tempList[0]);
            return list;
        }
        
        private static string ReadStreamFromResponse(WebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader sr = new StreamReader(responseStream))
            {
                //Need to return this response 
                string strContent = sr.ReadToEnd();
                return strContent;
            }
        }

    }
}
