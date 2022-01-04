using FBLACodingAndProgramming2021_2022.Geocoding;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace FBLACodingAndProgramming2021_2022.MVMM.View
{
    
    public partial class LocationView : UserControl
    {
        MainWindow form = Application.Current.Windows[0] as MainWindow;

        public string coordinates;
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
        //Address
        private void get_coords_button_Click(object sender, RoutedEventArgs e)
        {
            
            ClickButton(form.DistanceActivator);
            form.distance_button.IsChecked = true;
            form.location_button.IsChecked = false;

            Parameters.Longitude = Geocoder.GetCoordinatesAsync(address_text.Text).Result.ToArray()[1];
            Parameters.Latitude = Geocoder.GetCoordinatesAsync(address_text.Text).Result.ToArray()[0];
        }
        //Local Location
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var list=Geocoder.GetCoordinatesFromLocationSensor();

                Parameters.Latitude = list[0].ToString();
                Parameters.Longitude = list[1].ToString();
                MessageBox.Show(coordinates);
                ClickButton(form.DistanceActivator);
                form.distance_button.IsChecked = true;
                form.location_button.IsChecked = false;

            }
            catch(Exception)
            {
                MessageBox.Show("Error in getting local location, try one of the other options");
            }

            
        }
        //IP Address
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var coordinates = Geocoder.GetCoordinatesFromIpAddress();
            
            ClickButton(form.DistanceActivator);
            form.distance_button.IsChecked = true;
            form.location_button.IsChecked = false;

            Parameters.Longitude = Geocoder.GetCoordinatesAsync(address_text.Text).Result[0];
            Parameters.Latitude = Geocoder.GetCoordinatesAsync(address_text.Text).Result[1];
        }
    }
}
