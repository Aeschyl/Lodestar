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
    /// <summary>
    /// Interaction logic for AmenitiesView.xaml
    /// </summary>
    public partial class AmenitiesView : UserControl
    {
        //Allows access to components on Main Window
        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        public AmenitiesView()
        {
            InitializeComponent();
        }
        
        //Onward Button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Form.results_button.IsChecked = true;
            
        }
    }
}
