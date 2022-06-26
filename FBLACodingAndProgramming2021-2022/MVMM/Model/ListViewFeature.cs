using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FBLACodingAndProgramming2021_2022.MVMM.View;

namespace FBLACodingAndProgramming2021_2022.MVMM.Model
{
    public class ListViewFeature
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public string DistanceFromHome { get; set; }
        public string Temperature { get; set; }
        public BitmapImage Icon { get; set; }
        public BitmapImage Image { get; set; }
        public Json.Feature FeatureRef { get; set; }
        public string WeatherDescription { get; set; }
        public string FeatureName { get; set; }
        public Uri FavoriteHeart { get; set; }

    }
}
