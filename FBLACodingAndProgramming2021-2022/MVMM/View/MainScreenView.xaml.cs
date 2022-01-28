﻿using FBLACodingAndProgramming2021_2022.Geocoding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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
    /// Interaction logic for MainScreenView.xaml
    /// </summary>
    public partial class MainScreenView : UserControl
    {
        public MainScreenView()
        {
            InitializeComponent();
            
        }
        private async void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadRecomendations();
        }


        private async Task LoadRecomendations()
        {

            string json = await GetJsonStringRecomendationAsync();
            var values = JsonConvert.DeserializeObject<Root>(json);
            TopRightTextBox.Text = values.features[0].properties.name + "\n" + values.features[0].properties.address_line1 + values.features[0].properties.address_line2;
            TopLeftTextBox.Text = values.features[1].properties.name + "\n" + values.features[1].properties.address_line1 + values.features[1].properties.address_line2;
            BottomLeftTextBox.Text = values.features[2].properties.name + "\n" + values.features[2].properties.address_line1 + values.features[2].properties.address_line2;
            BottomRightTextBox.Text = values.features[3].properties.name + "\n" + values.features[3].properties.address_line1 + values.features[3].properties.address_line2;
        }

        private async Task<string> GetJsonStringRecomendationAsync()
        {
            var coords = await Geocoder.GetCoordinatesFromIpAddress();
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

        
    }
}