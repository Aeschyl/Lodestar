using Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FBLACodingAndProgramming2021_2022.MVMM.View
{
    /// <summary>
    /// Interaction logic for ResultsView.xaml
    /// </summary>
    public partial class ResultsView : UserControl
    {
        Root values;
        Feature selectedFeature;
        string jsonText;
        public ResultsView()
        {
            InitializeComponent();
            InitializeListBox();
            Clipboard.SetText(jsonText);


        }

        public Feature GetFeatureByName(string name)
        {
            foreach(Feature temp in values.features)
            {
                if (temp.properties.name.Equals(name))
                {
                    return temp;
                }
            }
            return null;
        }


        public void InitializeListBox()
        {
            Root.GetJsonFromGeoApi();
            jsonText = Root.jsonString;
            values = Root.FromJson(Root.jsonString);

             var list = new List<string>();

            
            
            
            
            foreach (Feature featureObject in values.features)
            {
                list.Add(featureObject.properties.name + "; " + Math.Round(featureObject.properties.distance / 1609.34, 2) + " mi");
            }
            MainListBox.ItemsSource = list;
            

        }

      

        private void MainListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectedFeature = GetFeatureByName(MainListBox.SelectedItem.ToString().Split(';')[0]);
            FeatureName.Text = selectedFeature.properties.name;
            FeatureInformation.Text = string.Format("{0}\nDistance: {1} mi", selectedFeature.properties.address_line2, Math.Round(selectedFeature.properties.distance/1609.34,2));

        }
    }
}
