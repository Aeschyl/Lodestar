using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text.Json;
using Json;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using System.Windows;

namespace FBLACodingAndProgramming2021_2022
{
    [Serializable]
    class Parameters 
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string category { get; set; }
        public static string subcategory { get; set; }
        public static List<string> amenities = new List<string>();
        public static string  Longitude { get; set; }
        public static string Latitude { get; set; }
        public static string radius { get; set; }





        


     



        public static void AddAmenities(string param) 
        {
            amenities.Add(param);
            log.Info("Added " + param + " amenity");
          

        }
        override public string ToString()
        {
            return "Category: " + category + ", Subcategory: " + subcategory + ", Amenities: " + string.Join(", ", amenities);
        }

        public static void ResetParameters()
        {
            category = string.Empty;
            subcategory = string.Empty;
            amenities.RemoveRange(0, amenities.Count);
            Longitude = string.Empty;
            Latitude = string.Empty;
            radius = string.Empty;

        }
    }
}
