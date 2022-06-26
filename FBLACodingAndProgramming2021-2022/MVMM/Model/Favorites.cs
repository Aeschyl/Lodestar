using System.Collections.Generic;

namespace FBLACodingAndProgramming2021_2022.MVMM.Model;

public class Favorites

{
    public List<Favorite> favorites { get; set; }
    public class Favorite
    {
        public string address { get; set; }
        public string imgLink { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string name { get; set; }
        public string placeID { get; set; }
    }
}