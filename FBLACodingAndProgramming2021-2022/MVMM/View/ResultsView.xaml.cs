﻿/*
    The logic for the Results screen, with a map, list of places, and details about a particular place
*/

using Aspose.Cells;
using Aspose.Cells.Utility;
using FBLACodingAndProgramming2021_2022.ErrorHandling;
using Json;
using Microsoft.Maps.MapControl.WPF;
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
        List<string> list = new List<string>();
        public ResultsView()
        {
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

        // Calls the API to receive data about the attractions as defined according to the user
        public async void InitializeListBox()
        {
            try
            {
                //Get Json String from Lodestar api
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
                //Make object from json string
                values = Root.FromJson(Root.jsonString);
            }
            catch (Exception)
            {
                log.Error("Error is parsing json string to object...server probably returned error");
                handler.ShowError("Something went wrong. Click the Restart button and try again");
                return;
            }

            

            if(values.features == null)
            {
                log.Error("No results");
                handler.ShowError("No Results, Click Restart");
                return;
            }

            int unknownCounter = 1;
            int repeatCounter = 1;
            for(int i =0;i < values.features.Count; i++)
            {


                if (values.features[i].properties.name == null)
                    {
                    values.features[i].properties.name = "Unamed " + values.features[i].properties.categories[0] + " Location " + unknownCounter;
                    unknownCounter++;
                    }
                
            }

            //Makes it so that program can differentiate by different names
            foreach(Feature f in values.features)
            {
                var duplicates = values.features.FindAll(e => e.properties.name.Equals(f.properties.name));
                if (duplicates.Count > 1)
                {
                    duplicates.ForEach(e =>
                    {
                        e.properties.name = e.properties.name + " #" + repeatCounter;
                        repeatCounter++;
                    });
                }
                repeatCounter = 1;
            }
            
            // Cycles through the objects to place them on the map
            foreach (Feature featureObject in values.features)
            {
                
                list.Add(featureObject.properties.name + "; " + Math.Round(featureObject.properties.distance / 1609.34, 2) + " mi");
            }
            MainListBox.ItemsSource = null;
            MainListBox.ItemsSource = list;
            Map.Visibility = Visibility.Visible;
            AddPushPinsToMap();

        }
        
        // Adds Markers to the Bing Maps generated in the Results screen
        private  void AddPushPinsToMap()
        {

            Pushpin currentLocation = new Pushpin();
            currentLocation.Location = new Location(double.Parse(Parameters.Latitude), double.Parse(Parameters.Longitude));
            currentLocation.Background = new BrushConverter().ConvertFrom("#0078d7") as SolidColorBrush;
            currentLocation.ToolTip = "Your Current Location\n(Might be innacurate if used Ip Address)";
            Map.Children.Add(currentLocation);
            Map.SetView(new Location(double.Parse(Parameters.Latitude), double.Parse(Parameters.Longitude)), 15);
            foreach(Feature f  in values.features)
            {
                
                Pushpin pin = new Pushpin();
                var location = new Location(f.properties.lat, f.properties.lon);
                pin.Location = location;
                pin.ToolTip = f.properties.name;
                Map.Children.Add(pin);
                var startingLocation = new Location(double.Parse(Parameters.Latitude), double.Parse(Parameters.Longitude));
                
                pin.MouseLeftButtonDown += delegate (object sender, MouseButtonEventArgs e)
                {
                    Map.SetView(new Location(((Pushpin) sender).Location.Latitude, ((Pushpin)sender).Location.Longitude), 15);
                    selectedFeature = GetFeatureByName(((Pushpin)sender).ToolTip.ToString());

                    FeatureInformation.Text = string.Format("{0}\n\n{1}\n\nDistance: {2} mi", selectedFeature.properties.name, selectedFeature.properties.address_line2, Math.Round(selectedFeature.properties.distance / 1609.34, 2));
                    GetWeather();
                };

            }
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

            Map.SetView(new Location(selectedFeature.properties.lat, selectedFeature.properties.lon), 20);

            FeatureInformation.Text = string.Format("{0}\n\n{1}\n\nDistance: {2} mi", selectedFeature.properties.name, selectedFeature.properties.address_line2, Math.Round(selectedFeature.properties.distance/1609.34,2));
            GetWeather();

        }

       
        // Gets weather for the place the user has selected. Helps inform the user of the current conditions in the location
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
            FeatureInformation.Text += FeatureInformation.Text.Contains("Weather")? string.Empty: string.Format("\n\nWeather: \nWind: {0}\nTemperature: {1}\nClouds: {2}", weather.wind.speed, weather.main.temp,weather.weather[0].description);
            
            
        }
        //Download Button
        private void DownloadResultsButton_Click(object sender, RoutedEventArgs e)
        {
            // create a blank Workbook object
            var workbook = new Workbook();

            // access default empty worksheet
            var worksheet = workbook.Worksheets[0];

            // set JsonLayoutOptions for formatting
            var layoutOptions = new JsonLayoutOptions();
            layoutOptions.ArrayAsTable = true;

            // import JSON data to CSV
            JsonUtility.ImportData(jsonText, worksheet.Cells, 0, 0, layoutOptions);

            string filePath = (System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LodestarResults", "Results[" + DateTime.Now.ToLongTimeString().Replace(":", "-").Replace(" ", "") + "].csv"));

            // Create directory temp1 if it doesn't exist
            Directory.CreateDirectory(Directory.GetParent(filePath).FullName);
            // save CSV file
            workbook.Save(filePath, SaveFormat.Csv);

            //Cleans up the json File
            RemoveColumnByIndex(filePath, 0);
            RemoveColumnByIndex(filePath, 0);

            Clipboard.SetText(filePath);
            MessageBox.Show("Path copied to clipboard");

        }

        // Cleans the data about the place to generate a human readable output report
        private void RemoveColumnByIndex(string path, int index)
        {
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                var line = reader.ReadLine();
                List<string> values = new List<string>();
                while (line != null)
                {
                    values.Clear();
                    var cols = line.Split(',');
                    for (int i = 0; i < cols.Length; i++)
                    {
                        if (i != index)
                            values.Add(cols[i]);
                    }
                    var newLine = string.Join(",", values);
                    lines.Add(newLine);
                    line = reader.ReadLine();
                }
            }

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }

        }

        //Sort list by alphabetical order
        private void AlphabeticalOrder_Click(object sender, RoutedEventArgs e)
        {
            values.features = values.features.OrderBy(x => x.properties.name).ToList();
            MainListBox.ItemsSource = values.features.Select(x => x.properties.name).ToList();
        }

        //Sort list by distance from the user
        private void DistanceFromHome_Click(object sender, RoutedEventArgs e)
        {
            
            
            values.features = values.features.OrderBy(x => x.properties.distance).ToList();
            list = values.features.Select(x => x.properties.name).ToList();
            MainListBox.ItemsSource = list;

        }

        

       
    }
}
