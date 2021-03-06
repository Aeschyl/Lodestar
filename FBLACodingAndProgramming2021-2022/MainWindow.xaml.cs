using FBLACodingAndProgramming2021_2022.Core;
using FBLACodingAndProgramming2021_2022.ErrorHandling;
using FBLACodingAndProgramming2021_2022.MVMM.View;
using FBLACodingAndProgramming2021_2022.MVMM.ViewModel;
using Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Reflection;
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


            
            var imagePath = AppContext.BaseDirectory + @"/Assets/LodestarIcon.png";


            LogoImage.ImageSource = new BitmapImage(new Uri(imagePath));

            ClickButton(TermsActivator);
            var result = CheckInternetConnectivity();
            
            
            
            if (!result)
            {
                StayInErrorScreen();
            }
            
            
            main_screen_button.IsChecked = true;



           


        }



        private void StayInErrorScreen()
        {
            /*ErrorRectangle.Visibility = Visibility.Visible;
            ErrorRectangle.Opacity = 20;*/
            //ErrorText.Visibility = Visibility.Visible;

            ErrorHandler handler = new ErrorHandler();

            handler.ShowError("No Internet Connection", true);
            

            Application.Current.MainWindow.IsEnabled = false;
            

            LoadingAnimation.Visibility = Visibility.Visible;


        }
        

        private bool CheckInternetConnectivity()
        {
            LoadingAnimation.Visibility = Visibility.Visible;
            var boolList = new List<bool>();
            for (int i = 0; i < 10; i++)
            {
                try
                {

                    var hostUrl = "www.google.com";

                    Ping ping = new Ping();

                    
                    PingReply result = ping.Send(hostUrl, 100);

                    boolList.Add( result.Status == IPStatus.Success);
                }
                catch (Exception)
                {
                    boolList.Add( false);
                }
            }
            LoadingAnimation.Visibility = Visibility.Hidden;
            return boolList.FindAll(e => e == true).Count > 0;
        }

      
        public static void ClickButton(Button b)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(b);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }

        public static void IncrementProgressBar(ProgressBar p, int val)
        {
            if (val == 100)
            {
                p.Foreground = new SolidColorBrush(Colors.SpringGreen);
            }
            else
            {
                p.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0078d7"));
            }
            p.Value = val;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            



        }

        private void amenities_button_Checked(object sender, RoutedEventArgs e)
        {
            IncrementProgressBar(MainProgressBar, 50);
        }

        private void results_button_Checked(object sender, RoutedEventArgs e)
        {
            IncrementProgressBar(MainProgressBar, 100);
        }

        private void category_button_Checked(object sender, RoutedEventArgs e)
        {
            IncrementProgressBar(MainProgressBar, 0);
        }

        private void sub_category_button_Checked(object sender, RoutedEventArgs e)
        {

            //"remembers" what the user selected for subcategory just in case the user wants to go back to this page
            switch (MainWindow.CurrentSubView)
            {
                case "have_fun":
                    ClickButton(HaveFunActivator);
                    //resets progress bar to 25
                    IncrementProgressBar(MainProgressBar, 25);
                    break;
                case "explore":
                    ClickButton(ExploreActivator);
                    //resets progress bar to 25
                    IncrementProgressBar(MainProgressBar, 25);
                    break;
                case "stay":
                    ClickButton(StayActivator);
                    //resets progress bar to 25
                    IncrementProgressBar(MainProgressBar, 25);
                    break;
                case "shop":
                    ClickButton(ShopActivator);
                    //resets progress bar to 25
                    IncrementProgressBar(MainProgressBar, 25);
                    break;
                case "eat":
                    ClickButton(EatActivator);
                    //resets progress bar to 25
                    IncrementProgressBar(MainProgressBar, 25);
                    break;
                default:
                    MessageBox.Show("You must select a Category first!");
                    sub_category_button.IsChecked = false;
                    category_button.IsChecked = true;
                    ClickButton(CategoryActivator);
                    break;


            }
        }

        private void location_button_Checked(object sender, RoutedEventArgs e)
        {
            IncrementProgressBar(MainProgressBar, 66);
            ClickButton(LocationActivator);

        }

        private void distance_button_Checked(object sender, RoutedEventArgs e)
        {
            ClickButton(DistanceActivator);
            IncrementProgressBar(MainProgressBar, 83);
        }
        
        //Close Button
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //Restart Button
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            category_button.IsChecked = true;
            Parameters.ResetParameters();
            
            

            
        }

        private void faq_button_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
