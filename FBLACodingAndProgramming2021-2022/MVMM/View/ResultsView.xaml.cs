using CefSharp;
using CefSharp.Wpf;
using FBLACodingAndProgramming2021_2022.ErrorHandling;
using Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        Root values;
        Feature selectedFeature;
        string jsonText;
        ErrorHandler handler;
        public ResultsView()
        {
            var settings = new CefSettings();

            // Disable GPU in WPF and Offscreen examples until #1634 has been resolved
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            

            Cef.Initialize(settings);
            

            InitializeComponent();
            handler = new ErrorHandler();
            InitializeListBox();
            
            

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


        public async void InitializeListBox()
        {
            

            try
            {
                await Root.GetJsonFromGeoApiAsync();
            }
            catch (Exception)
            {
                log.Error("Error with getting json from api");
                handler.ShowError("Something went wrong. Click the Restart button and try again");
            }
            jsonText = Root.jsonString;
            try
            {
                values = Root.FromJson(Root.jsonString);
            }
            catch (Exception)
            {
                log.Error("Error is parsing json string to object...server probably returned error");
                handler.ShowError("Something went wrong. Click the Restart button and try again");
                return;
            }

             var list = new List<string>();

            if(values.features == null)
            {
                log.Error("No results");
                handler.ShowError("No Results, Click Restart");
                return;
            }

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
            MainListBox.ItemsSource = null;
            MainListBox.ItemsSource = list;



            MainWebBrowser.Address = "https://mapapi-1.sami200.repl.co/map?userLat=39&userLong=-104&coords=39.59769398357406,-104.89756350239023,Pindustry,39.59617284146872,-104.87966780541137,Ch/n%20ick-Fil-Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            

        }


      

        private void MainListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MainListBox.Items.Count < 1)
            {
                log.Debug("Got No Results From Api");
                return;
            }
                //31
            selectedFeature = GetFeatureByName(MainListBox.SelectedItem.ToString().Split(';')[0]);
            
            FeatureInformation.Text = string.Format("{0}\n\n{1}\n\nDistance: {2} mi", selectedFeature.properties.name, selectedFeature.properties.address_line2, Math.Round(selectedFeature.properties.distance/1609.34,2));
            GetWeather();

        }

       

        private async void GetWeather()
        {
            if(selectedFeature == null)
            {
                new ErrorHandler().ShowError("No Place Selected");
                return;
            }
            var url = new StringBuilder(@"https://touristserver.sami200.repl.co/weather?");
            url.Append("long=");
            url.Append(selectedFeature.properties.lon);
            url.Append("&lat=");
            url.Append(selectedFeature.properties.lat);

            string outputString = "";
            var request = new HttpClient();
            try
            {
                string response = await request.GetStringAsync(url.ToString());



                outputString = response;


            }
            catch (Exception)
            {
                new ErrorHandler().ShowError("Something went wrong with getting weather information");
                return;
            }

            var weather = JsonConvert.DeserializeObject<WeatherInfo.Root>(outputString);
            FeatureInformation.Text += string.Format("\n\nWeather: \nWind: {0}\nTemperature: {1}\nClouds: {2}", weather.wind.speed, weather.main.temp,weather.weather[0].description);
            
            
        }
    }
}
