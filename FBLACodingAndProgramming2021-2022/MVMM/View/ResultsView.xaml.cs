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
        public ResultsView()
        {
            InitializeComponent();
            InitializeListBox();

        }


        public void InitializeListBox()
        {
            Root.GetJsonFromGeoApi();

            Root values = Root.FromJson(Root.jsonString);

             var list = new List<string>();

            
            
            
            
            foreach (Feature featureObject in values.features)
            {
                list.Add(featureObject.properties.name + ", " + Math.Round(featureObject.properties.distance / 1609.34, 2) + " mi");
            }
            MainListBox.ItemsSource = list;
            

        }

        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
