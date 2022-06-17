/*
    The logic for the location screen
*/

using FBLACodingAndProgramming2021_2022.ErrorHandling;
using FBLACodingAndProgramming2021_2022.Geocoding;
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
        private (string, string)? coordinatesTuple;
        MainWindow form = Application.Current.Windows[0] as MainWindow;
        ErrorHandling.ErrorHandler handler = new ErrorHandling.ErrorHandler();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        
        public LocationView()
        {
            InitializeComponent();
            

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

        // Manually entered Address
        private async void get_coords_button_Click(object sender, RoutedEventArgs e)
        {
            
            
            form.distance_button.IsChecked = true;
            form.location_button.IsChecked = false;
            try
            {
                log.Debug("Inputted address: " + address_text.Text);
                var arr = await Geocoder.GetCoordinatesAsync(address_text.Text);

                Parameters.Longitude = arr[1];
                Parameters.Latitude = arr[0];
                ClickButton(form.DistanceActivator);
            }
            catch (Exception error)
            {
                log.Error("Could not get coordinates from address; " + error.Message);
                handler.ShowError("Something went wrong, try one of the other options");
                ClickButton(form.LocationActivator);
            }
            
        }

        // Uses Zipcode
        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            form.distance_button.IsChecked = true;
            form.location_button.IsChecked = false;
            try
            {
                log.Debug("Inputted address: " + zipcode_text.Text);
                var arr = await Geocoder.GetCoordinatesAsync(zipcode_text.Text);

                Parameters.Longitude = arr[1];
                Parameters.Latitude = arr[0];
                ClickButton(form.DistanceActivator);
            }
            catch (Exception error)
            {
                log.Error("Could not get coordinates from address; " + error.Message);
                handler.ShowError("Something went wrong, try one of the other options");
                ClickButton(form.LocationActivator);
            }

        }

        // IP Address Geocoding
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {



            if (coordinatesTuple.HasValue)
            {
                (Parameters.Longitude, Parameters.Latitude) = coordinatesTuple.Value;
                return;
            }



            var coordinates = await Geocoder.GetCoordinatesFromIpAddress();
                if (coordinates != null)
                {
                    Parameters.Latitude = coordinates[1];
                    Parameters.Longitude = coordinates[0];
                    coordinatesTuple = (coordinates[0], coordinates[1]);
                }
                else
                {
                    new ErrorHandler().ShowError("An Error Occurred with getting Ip Address\nTry Another option or try again");
                    e.Handled = true;
                    return;
                }

            ClickButton(form.DistanceActivator);
            form.distance_button.IsChecked = true;
            form.location_button.IsChecked = false;


        }


        private void Zipcode_text_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter)
                return;
            Button_Click(null, null);
            e.Handled = true;
        }
    }
}
