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
    /// Interaction logic for ExploreSubView.xaml
    /// </summary>
    public partial class ExploreSubView : UserControl
    {
        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        public ExploreSubView()
        {
            InitializeComponent();
        }

        public static void ClickButton(Button b)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(b);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        //Nature Button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClickButton(Form.AmenitiesActivator);
            Form.amenities_button.IsChecked = true;

            Parameters.category = "natural";
        }
        //Popular Attractions Button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ClickButton(Form.AmenitiesActivator);
            Form.amenities_button.IsChecked = true;

            Parameters.subcategory = "attraction";
        }
        //Camping Button
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ClickButton(Form.AmenitiesActivator);
            Form.amenities_button.IsChecked = true;

            Parameters.category = "camping";

        }
        //Popular Sights Button
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ClickButton(Form.AmenitiesActivator);
            Form.amenities_button.IsChecked = true;

            Parameters.subcategory = "sights";
        }
        //Beach Button
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ClickButton(Form.AmenitiesActivator);
            Form.amenities_button.IsChecked = true;

            Parameters.category = "beach";
        }
        //Skip Button
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ClickButton(Form.AmenitiesActivator);
            Form.amenities_button.IsChecked = true;
        }
    }
}
