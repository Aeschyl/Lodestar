/*
    The logic for the distance screen that enables the user to select how far they are willing to go
*/

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
    /// Interaction logic for DistanceView.xaml
    /// </summary>
    public partial class DistanceView : UserControl
    {
        MainWindow form = Application.Current.Windows[0] as MainWindow;
        public DistanceView()
        {
            InitializeComponent();
            SliderValue.Text = "1";
        }
        public static void ClickButton(Button b)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(b);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        // A method to manage the slider in the distance screen
        private void Slider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            SliderValue.Text = (Slider.Value * 5).ToString("F0");
            if(Slider.Value < 1)
            {
                SliderValue.Text = "1";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClickButton(form.ResultsActivator);
            Parameters.radius = SliderValue.Text;
            form.results_button.IsChecked = true;
            form.distance_button.IsChecked = false;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
