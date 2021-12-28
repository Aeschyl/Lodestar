using FBLACodingAndProgramming2021_2022.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace FBLACodingAndProgramming2021_2022
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static string CurrentSubView { get; set; }
        Parameters parameter = new Parameters();
        public MainWindow()
        {
            InitializeComponent();

            
            

        }
        public static void ClickButton(Button b)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(b);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        public static void IncrementProgressBar(ProgressBar p, int val)
        {
            p.Value = val;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PythonExecuter.executePythonEXE();
        }

        private void amenities_button_Checked(object sender, RoutedEventArgs e)
        {
            IncrementProgressBar(MainProgressBar, 50);
        }

        private void results_button_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Clicked");
        }

        private void category_button_Checked(object sender, RoutedEventArgs e)
        {
            IncrementProgressBar(MainProgressBar,0);
        }

        private void sub_category_button_Checked(object sender, RoutedEventArgs e)
        {
            //resets progress bar to 25
            IncrementProgressBar(MainProgressBar, 25);
            //"remembers" what the user selected for subcategory just in case the user wants to go back to this page
            switch (MainWindow.CurrentSubView) 
            {
                case "have_fun":
                    ClickButton(HaveFunActivator);
                    break;
                case "explore":
                    ClickButton(ExploreActivator);
                    break;
                case "stay":
                    ClickButton(StayActivator);
                    break;
                case "shop":
                    ClickButton(ShopActivator);
                    break;
                case "eat":
                    ClickButton(EatActivator);
                    break;
                default:
                    MessageBox.Show("You must select a Category first!");
                    break;


            }
        }
    }
}
