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

        public static List<string> GetCoordinatesAsync(string address) 

        {
            string result;

            //Connects to our own third party server so that api keys are safe
            var requestsServerUrl = new StringBuilder(@"https://touristserver.sami200.repl.co/bingmaps?");

            var requestUrl = new StringBuilder(@"https://dev.virtualearth.net/REST/v1/Locations?q=");

            requestsServerUrl.Append(@"url=");

            

            requestUrl.Append(address);

            
            requestsServerUrl.Append(HttpUtility.UrlEncode(requestUrl.ToString()));

            var request = (HttpWebRequest)WebRequest.Create(requestsServerUrl.ToString());

            using (HttpWebResponse response =  (HttpWebResponse) request.GetResponse())
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

        public static List<string> GetCoordinatesFromLocationSensor()
        {
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

            // Do not suppress prompt, and wait 1000 milliseconds to start.
            watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));

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

        private static string GetIPAddressAsync()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            request.Proxy = null;
            using (WebResponse response =  request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }

        public static List<string> GetCoordinatesFromIpAddress()
        {
            string ipAddress = GetIPAddressAsync();
            //Get Ip Address
            


            string jsonString;
            string html;
            StringBuilder url = new StringBuilder(@"https://ipinfo.io/");
            url.Append(ipAddress);
            url.Append("/json?token=afafcbd31ed7de");
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

            Task<WebResponse> task = Task.Factory.FromAsync(
        request.BeginGetResponse,
        asyncResult => request.EndGetResponse(asyncResult),
        (object)null);
            

            jsonString = task.ContinueWith(t => ReadStreamFromResponse(t.Result)).Result;

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
