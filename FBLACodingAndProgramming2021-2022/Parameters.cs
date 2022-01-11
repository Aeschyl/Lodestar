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

namespace FBLACodingAndProgramming2021_2022
{
    [Serializable]
    class Parameters 
    {
        public static string category { get; set; }
        public static string subcategory { get; set; }
        public static List<string> amenities { get; set; }
        public static string  Longitude { get; set; }
        public static string Latitude { get; set; }
        public static string radius { get; set; }




        public Parameters()
        {
            category = "";
            subcategory = "";
            amenities = new List<string>();
        }

        public void Serialize()
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory().Substring(0, System.IO.Directory.GetCurrentDirectory().IndexOf("bin") - 1);


            File.WriteAllText(currentDirectory + @"/Python/src/Input/Input.json", JsonSerializer.Serialize(this));
        }
        public Parameters DeSerialize() 
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory().Substring(0, System.IO.Directory.GetCurrentDirectory().IndexOf("bin") - 1);
            return JsonSerializer.Deserialize<Parameters>(File.ReadAllText(currentDirectory + @"/Python/src/Input/Input.json"));
        
        }

     



        public static void AddAmenities(string param) 
        {
            amenities.Add(param);
        
        }
        override public string ToString()
        {
            return "Category: " + category + ", Subcategory: " + subcategory + ", Amenities: " + string.Join(", ", amenities);
        }

        public static void ResetParameters()
        {
            category = string.Empty;
            subcategory = string.Empty;
            amenities = null;
            Longitude = string.Empty;
            Latitude = string.Empty;
            radius = string.Empty;

        }
    }
}
