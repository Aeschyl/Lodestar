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

        public List<CheckBox> checkBoxes;
        public AmenitiesView()
        {
            InitializeComponent();
            checkBoxes = new List<CheckBox>(0);
        }
        public static void ClickButton(Button b)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(b);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        private void GetCheckBoxes()
        {
            //Looping through first stack panel
            foreach(CheckBox child in StackPanelOne.Children)
            {
                checkBoxes.Add(child);
            }
            //Looping through second stack panel
            foreach (CheckBox child in StackPanelTwo.Children)
            {
                checkBoxes.Add(child);
            }
            //Looping through thirst stack panel
            foreach (CheckBox child in StackPanelOne.Children)
            {
                checkBoxes.Add(child);
            }

        
        } 

        private void ResetCheckBoxes()
        {
            GetCheckBoxes();
            foreach(CheckBox value in checkBoxes)
            {
                value.IsChecked = false;
            }
        }
        

        //Onward Button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Form.location_button.IsChecked = true;
            Form.amenities_button.IsChecked = false;

            ClickButton(Form.LocationActivator);
            //Makes all of the CheckBoxes Unselected
            ResetCheckBoxes();

        }
        //Dog Friendly
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Parameters.AddAmenities("dogs");
        }
        //WheelChair
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            Parameters.AddAmenities("wheelchair");
        }
        //Internet Access
        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {
            Parameters.AddAmenities("internet_access");
        }
        //No Fee
        private void CheckBox_Checked_3(object sender, RoutedEventArgs e)
        {
            Parameters.AddAmenities("no_fee");
        }
    }
}
