namespace Json
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Text;
    using FBLACodingAndProgramming2021_2022;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

   
        
        
        internal class Datasource
        {
            public string sourcename { get; set; }
            public string attribution { get; set; }
            public string license { get; set; }
            public string url { get; set; }
        }

        public class Properties
        {
            public string name { get; set; }
            public string street { get; set; }
            public string city { get; set; }
            public string county { get; set; }
            public string state { get; set; }
            public string postcode { get; set; }
            public string country { get; set; }
            public string country_code { get; set; }
            public double lon { get; set; }
            public double lat { get; set; }
            public string formatted { get; set; }
            public string address_line1 { get; set; }
            public string address_line2 { get; set; }
            public List<string> categories { get; set; }
            public List<string> details { get; set; }
            public int distance { get; set; }
            public string place_id { get; set; }
            public string housenumber { get; set; }
            public string suburb { get; set; }
        }

        public class Geometry
        {
            public string type { get; set; }
            public List<double> coordinates { get; set; }
        }

        public class Feature
        {
            public string type { get; set; }
            public Properties properties { get; set; }
            public Geometry geometry { get; set; }
        }

        public class Root
        {
            public string type { get; set; }
            public List<Feature> features { get; set; }
            public static string jsonString { get; set; }

        public static void GetJsonFromGeoApi()
        {
            string html;
            string url = @"https://api.geoapify.com/v2/places/?";


            StringBuilder builder = new StringBuilder();
            builder.Append(url);
            builder.Append("categories=");
            builder.Append(Parameters.category);
            if (!Parameters.subcategory.Equals(string.Empty))
            {
                builder.Append("." + Parameters.subcategory);
            }

            builder.Append("&");

            var conditions = new StringBuilder();

            if (Parameters.amenities.Count > 1)
            {
                foreach (string temp in Parameters.amenities)
                {
                    conditions.Append(temp + ", ");
                }
                conditions.Remove(conditions.Length - 1, conditions.Length);
                builder.Append("conditions=");
                builder.Append(conditions);
                builder.Append("&");
            }
            else if (Parameters.amenities.Count == 1)
            {
                conditions.Append(Parameters.amenities[0]);
                builder.Append("conditions=");
                builder.Append(conditions);
                builder.Append("&");

            }

            builder.Append("filter=circle:");
            builder.Append(Parameters.Longitude + ",");
            builder.Append(Parameters.Latitude + ",");
            builder.Append(int.Parse(Parameters.radius) * 1609);

            builder.Append("&");
            builder.Append("bias=proximity:");
            builder.Append(Parameters.Longitude);
            builder.Append("," + Parameters.Latitude);

            builder.Append("&");
            builder.Append("limit=10");

            builder.Append("&");
            builder.Append("apiKey=");
            //Need to hide this somehow
            builder.Append("233315ef9be94184b7addc54c006bbde");


            var request = (HttpWebRequest)WebRequest.Create(builder.ToString());

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            jsonString = html;
        }

        public static Root FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Root>(json);
        }
    }

        

        
    
}
