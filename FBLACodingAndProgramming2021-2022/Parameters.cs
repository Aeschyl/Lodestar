using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text.Json;

namespace FBLACodingAndProgramming2021_2022
{
    [Serializable]
    class Parameters 
    {
        public string category { get; set; }
        public string subcategory { get; set; }
        public List<string> amenities { get; set; }

        

        public Parameters()
        {
            category = "";
            subcategory = "";
            amenities = new List<string>();
        }

        public void Serialize()
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory().Substring(0, System.IO.Directory.GetCurrentDirectory().IndexOf("bin") - 1);


            File.WriteAllText(currentDirectory + @"/Python/src/Input/input.json", JsonSerializer.Serialize(this));
        }
        public Parameters DeSerialize() 
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory().Substring(0, System.IO.Directory.GetCurrentDirectory().IndexOf("bin") - 1);
            return JsonSerializer.Deserialize<Parameters>(File.ReadAllText(currentDirectory + @"/Python/src/Input/input.json"));
        
        }



        public void AddAmenities(string param) 
        {
            amenities.Add(param);
        
        }
        override
        public string ToString()
        {
            return "Category: " + category + ", Subcategory: " + subcategory + ", Amenities: " + string.Join(", ", amenities);
        }
    }
}
