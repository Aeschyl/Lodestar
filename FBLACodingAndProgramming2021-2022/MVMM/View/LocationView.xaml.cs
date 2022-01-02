using FBLACodingAndProgramming2021_2022.Geocoding;
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
    /// Interaction logic for LocationView.xaml
    /// </summary>
    public partial class LocationView : UserControl
    {
        public string coordinates;
        public LocationView()
        {
            InitializeComponent();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter)
                return;

            // your event handler here
            get_coords_button_Click(null, null);
            
            e.Handled = true;
        }

        private void get_coords_button_Click(object sender, RoutedEventArgs e)
        {
            coordinates = Geocoder.GetCoordinatesAsync(address_text.Text).Result;
            MessageBox.Show(coordinates);
        }
    }
}
