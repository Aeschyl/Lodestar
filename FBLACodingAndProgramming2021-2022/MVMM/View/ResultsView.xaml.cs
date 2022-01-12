using FBLACodingAndProgramming2021_2022.ErrorHandling;
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
        ErrorHandler handler;
        public ResultsView()
        {
            InitializeComponent();
            handler = new ErrorHandler();
            InitializeListBox();
            //Clipboard.SetText(jsonText);
            

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
            

            try
            {
                Root.GetJsonFromGeoApi();
            }
            catch (Exception)
            {
                handler.ShowError("Address Was Not Found");
            }
            jsonText = Root.jsonString;
            try
            {
                values = Root.FromJson(Root.jsonString);
            }
            catch (Exception)
            {
                handler.ShowError("Something went wrong. Click the Restart button and try again");
                return;
            }

             var list = new List<string>();


            int unknownCounter = 1;
            for(int i =0;i < values.features.Count; i++)
            {
                if (values.features[i].properties.name == null)
                    {
                    values.features[i].properties.name = "Unamed " + values.features[i].properties.categories[0] + " Location " + unknownCounter;
                    unknownCounter++;
                    }
            }
            
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
