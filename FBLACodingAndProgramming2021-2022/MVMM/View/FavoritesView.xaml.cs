/*
    The logic for the favorites screen: list of places, and details about a particular place
*/

using Aspose.Cells;
using Aspose.Cells.Utility;
using BingMapsRESTToolkit;
using FBLACodingAndProgramming2021_2022.ErrorHandling;
using FBLACodingAndProgramming2021_2022.MVMM.Model;
using Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Aspose.Cells.Drawing;
using Button = System.Windows.Controls.Button;

namespace FBLACodingAndProgramming2021_2022.MVMM.View
{
    /// <summary>
    /// Interaction logic for FavoritesView.xaml
    /// </summary>
    public partial class FavoritesView : UserControl
    {
        private readonly object _filledHeartIcon; 
        private readonly string _serverUrl = "https://touristserver.sami200.repl.co/";
        private readonly object _hollowHeartIcon;
        private readonly HttpClient _client;
        private readonly ErrorHandler _handler;
        private HashSet<string> _favoritesId;
        private HashSet<Favorite> _favorites;
        private readonly string _cpuSerialId = GetCpuSerialNumber();
        public FavoritesView()
        {
            _filledHeartIcon = Application.Current.Resources["FilledHeartIcon"];
            _hollowHeartIcon = Application.Current.Resources["HollowHeartIcon"];
            _client = new();
            _handler = new();
            
            InitializeComponent();
        }

        private async void MainListBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            
            var favorites = JsonConvert
                .DeserializeObject<Favorites>(await _client.GetStringAsync(
                    _serverUrl + $@"getFavorites?cpuserialid={_cpuSerialId}")) ?? new Favorites();
            _favorites = favorites.favorites.ToHashSet();
            MainListBox.ItemsSource = _favorites;
            _favoritesId = _favorites.Select(x => x.PlaceId)
                .ToHashSet();
        }

        private void MainListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            

            
            var listViewFeature = (MainListBox.SelectedItem as ListViewFeature);
            //31
            
            
            Clipboard.SetText(listViewFeature.FeatureRef.properties.formatted);
            new ErrorHandler().ShowError("Copied Address", color: new SolidColorBrush(Colors.SpringGreen));
            


            
            



        }

        private static string GetCpuSerialNumber()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["processorID"].Value.ToString();
                break;
            }
            return cpuInfo;
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var serialNumberTask = Task.Run(GetCpuSerialNumber);
            var b = sender as Button;
            
            var client = new HttpClient();
            var isFavorite = _favoritesId.Contains(b.ToolTip.ToString());
            
            
            var serialNumber = await serialNumberTask;
            
            var removeUrl = @$"https://touristserver.sami200.repl.co/removeFavorite?cpuserialid={serialNumber}";
            
                var values = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    { "placeID", b.ToolTip.ToString() }
                });
                
                var response = await client.PostAsync(removeUrl, values);
                if (response.IsSuccessStatusCode)
                {
                    _handler.ShowError("Removed from favorites", color:new SolidColorBrush(Colors.SpringGreen));
                    b.Content = _hollowHeartIcon;
                    _favoritesId.Remove(b.ToolTip.ToString());
                    _favorites.RemoveWhere(x => x.PlaceId == b.ToolTip.ToString());
                    MainListBox.ItemsSource = null;
                    MainListBox.ItemsSource = _favorites;

                }
                else
                {
                    _handler.ShowError("Error with removing from favorites");
                }
            
            
        }
    }
    }
