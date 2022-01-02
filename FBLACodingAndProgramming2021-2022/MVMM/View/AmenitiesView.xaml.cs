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
        public static void ClickButton(Button b)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(b);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        //Onward Button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Form.location_button.IsChecked = true;
            Form.amenities_button.IsChecked = false;

            ClickButton(Form.LocationActivator);
        }
    }
}
