/*
    The logic for the home screen that contains recommendations for the user
*/

using FBLACodingAndProgramming2021_2022.Geocoding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FBLACodingAndProgramming2021_2022.ErrorHandling;

namespace FBLACodingAndProgramming2021_2022.MVMM.View
{
    /// <summary>
    /// Interaction logic for MainScreenView.xaml
    /// </summary>
    public partial class MainScreenView : UserControl
    {
        MainWindow form = Application.Current.Windows[0] as MainWindow;
        public MainScreenView()
        {
            InitializeComponent();
            

        }
        private async void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            FindAttractionsButton.IsEnabled = false;
            FindAttractionsButton.Visibility = Visibility.Hidden;
            form.LoadingAnimation.Visibility = Visibility.Visible;
            TopLeftBorder.Visibility = Visibility.Hidden;
            TopRightBorder.Visibility = Visibility.Hidden;
            BottomLeftBorder.Visibility = Visibility.Hidden;
            BottomRightBorder.Visibility = Visibility.Hidden;
            await LoadRecomendations();
            TopLeftBorder.Visibility = Visibility.Visible;
            TopRightBorder.Visibility = Visibility.Visible;
            BottomLeftBorder.Visibility = Visibility.Visible;
            BottomRightBorder.Visibility = Visibility.Visible;
            form.LoadingAnimation.Visibility = Visibility.Hidden;
            FindAttractionsButton.Visibility = Visibility.Visible;
            FindAttractionsButton.IsEnabled = true;
        }


        // Loading in the recommendations for the user based on their search behavior
        private async Task LoadRecomendations()
        {

            string json = await GetJsonStringRecomendationAsync();
            if (json.Contains("Error") || json.Length == 0)
            {
                Clipboard.SetText(json);
                TopRightTextBox.Text = "No recomendations yet";
                TopLeftTextBox.Text = "No recomendations yet";
                BottomLeftTextBox.Text = "No recomendations yet";
                BottomRightTextBox.Text = "No recomendations yet";
                return;
            }
            var values = JsonConvert.DeserializeObject<Root>(json); // Converting the recommendations
            Dictionary<int, BitmapImage> images = new Dictionary<int, BitmapImage>();
            var counter = 0;
            values.features.ForEach(e =>
            {
                Uri outUri;
                if(Uri.TryCreate(e.properties.imgLink, UriKind.Absolute, out outUri))
                {
                    images.Add(counter, new BitmapImage(outUri));
                }
                counter++;
            });
            var listOfImages = new List<Image>{Image1, Image2, Image3, Image4 };

            counter = 0;
            listOfImages.ForEach(e =>
            {
                e.Source = images.ContainsKey(counter)? images[counter] : null;
                counter++;
            });
            
            TopLeftTextBox.Text = values.features[0].properties.formatted.Replace(",", "\n");
            TopRightTextBox.Text = values.features[1].properties.formatted.Replace(",", "\n");
            BottomLeftTextBox.Text = values.features[2].properties.formatted.Replace(",", "\n");
            BottomRightTextBox.Text = values.features[3].properties.formatted.Replace(",", "\n");
        }

        // Calling the lodestar server for recommendations for the user
        private async Task<string> GetJsonStringRecomendationAsync()
        {
            var coords = await Geocoder.GetCoordinatesFromIpAddress();

            if(coords == null)
            {
                return "Error";
            }

            var url = new StringBuilder(@"https://touristserver.sami200.repl.co/recommendations?");
            url.Append("cpuserialid=");
            url.Append(GetCPUSerialNumber());

            url.Append("&long=");
            url.Append(coords[0]);
            url.Append("&lat=");
            url.Append(coords[1]);

            var client = new HttpClient();
            string json = await client.GetStringAsync(url.ToString());
            return json;
        }

        // Obtaining the user's CPU Serial Number to track their data without directly linking or making them create an account
        private static string GetCPUSerialNumber()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["processorID"].Value.ToString();
                break;
            }
            return cpuInfo;
        }

        internal class Datasource
        {
            public string sourcename { get; set; }
            public string attribution { get; set; }
            public string license { get; set; }
            public string url { get; set; }
        }

        public class Properties
        {
            public string imgLink { get; set; }
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
        }
        public static void ClickButton(Button b)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(b);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }
        //Get Recomendations Button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClickButton(form.CategoryActivator);
            form.category_button.IsChecked = true;
            form.main_screen_button.IsChecked = false;
        }

        private void TopLeftBorder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(TopLeftTextBox.Text);
            new ErrorHandler().ShowError(err : "copied", color : new SolidColorBrush(Colors.SpringGreen));
        }

        private void TopRightBorder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(TopRightTextBox.Text);
            new ErrorHandler().ShowError(err : "copied", color : new SolidColorBrush(Colors.SpringGreen));
        }


        private void BottomLeftBorder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(BottomLeftTextBox.Text);
            new ErrorHandler().ShowError(err : "copied", color : new SolidColorBrush(Colors.SpringGreen));
        }

        private void BottomRightBorder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(BottomRightTextBox.Text);
            new ErrorHandler().ShowError(err : "copied", color : new SolidColorBrush(Colors.SpringGreen));
        }
    }
}
