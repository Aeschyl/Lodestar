﻿using FBLACodingAndProgramming2021_2022.Geocoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace FBLACodingAndProgramming2021_2022.MVMM.View
{
    
    public partial class LocationView : UserControl
    {
        MainWindow form = Application.Current.Windows[0] as MainWindow;
        ErrorHandling.ErrorHandler handler = new ErrorHandling.ErrorHandler();
        string ipAddressLongitude;
        string ipAddressLatitude;


        public string coordinates;
        public LocationView()
        {
            InitializeComponent();
            IpAddressCoordinates();

        }

        public static void ClickButton(Button b)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(b);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter)
                return;

            
            get_coords_button_Click(null, null);
            
            e.Handled = true;
        }
        //Address
        private void get_coords_button_Click(object sender, RoutedEventArgs e)
        {
            
            
            form.distance_button.IsChecked = true;
            form.location_button.IsChecked = false;
            try
            {
                var arr = Geocoder.GetCoordinatesAsync(address_text.Text).ToArray();
                Parameters.Longitude = arr[1];
                Parameters.Latitude = arr[0];
                ClickButton(form.DistanceActivator);
            }
            catch (Exception)
            {
                handler.ShowError("Something went wrong, try one of the other options");
                ClickButton(form.LocationActivator);
            }
            
        }
        //Local Location
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                var list=Geocoder.GetCoordinatesFromLocationSensor();

                Parameters.Latitude = list[0].ToString();
                Parameters.Longitude = list[1].ToString();
                ClickButton(form.DistanceActivator);
                form.distance_button.IsChecked = true;
                form.location_button.IsChecked = false;
                

            }
            catch(Exception)
            {
                
                handler.ShowError("Something went wrong, try one of the other options");
                
                
            }

            
        }
        //IP Address
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            
            ClickButton(form.DistanceActivator);
            form.distance_button.IsChecked = true;
            form.location_button.IsChecked = false;

            Parameters.Longitude = ipAddressLongitude;
            Parameters.Latitude = ipAddressLatitude;
        }

        private void IpAddressCoordinates()
        {
            var coordinates = Geocoder.GetCoordinatesFromIpAddress();
            ipAddressLatitude = coordinates[1];
            ipAddressLongitude = coordinates[0];
        }

       
    }
}
