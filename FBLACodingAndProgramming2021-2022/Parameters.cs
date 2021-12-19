using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace FBLACodingAndProgramming2021_2022
{
    [Serializable]
    class Parameters 
    {
        public string category { get; set; }
        public string subcategory { get; set; }
        public List<string> amenities { get; set; }

        string currentDirectory = System.IO.Directory.GetCurrentDirectory().Substring(0, System.IO.Directory.GetCurrentDirectory().IndexOf("bin") - 1);

        public Parameters()
        {
            category = "";
            subcategory = "";
            amenities = new List<string>();
        }

        public void Serialize()
        {
            
            
            Stream stream = new FileStream(currentDirectory + @"/Python/src/Input/input.json", FileMode.Create, FileAccess.Write);
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Close();
        }
        public Parameters DeSerialize() 
        {
            using (var stream = new FileStream(currentDirectory + @"/Python/src/Output/output.json", FileMode.Open, FileAccess.Read)) 
            {
                var formatter = new BinaryFormatter();
                return (Parameters)formatter.Deserialize(stream);
            }
        
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
