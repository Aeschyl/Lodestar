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
    /// Interaction logic for HaveFunSubView.xaml
    /// </summary>
    public partial class HaveFunSubView : UserControl
    {
        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        public HaveFunSubView()
        {
            InitializeComponent();
        }
        public static void ClickButton(Button b)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(b);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }


        //Museum Button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClickButton(Form.AmenitiesActivator);
            Form.amenities_button.IsChecked = true;
        }
        //Theme Button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ClickButton(Form.AmenitiesActivator);
            Form.amenities_button.IsChecked = true;
        }
        //Cinema Button
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ClickButton(Form.AmenitiesActivator);
            Form.amenities_button.IsChecked = true;
        }
        //Aquarium Button
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ClickButton(Form.AmenitiesActivator);
            Form.amenities_button.IsChecked = true;
        }
        //Zoo Button
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ClickButton(Form.AmenitiesActivator);
            Form.amenities_button.IsChecked = true;
        }
        //Theater Button
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ClickButton(Form.AmenitiesActivator);
            Form.amenities_button.IsChecked = true;
        }
    }
}
