/*
    The logic for the Results screen, with a map, list of places, and details about a particular place
*/

using Aspose.Cells;
using Aspose.Cells.Utility;
using BingMapsRESTToolkit;
using FBLACodingAndProgramming2021_2022.ErrorHandling;
using FBLACodingAndProgramming2021_2022.MVMM.Model;
using Json;
using Microsoft.Maps.MapControl.WPF;
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
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace FBLACodingAndProgramming2021_2022.MVMM.View
{
    /// <summary>
    /// Interaction logic for ResultsView.xaml
    /// </summary>
    public partial class ResultsView : UserControl
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        readonly List<Feature> _routeWaypoints = new List<Feature>();
        readonly MainWindow _form = Application.Current.Windows[0] as MainWindow;
        Root _values;
        private HashSet<string> _favorites;
        Feature _selectedFeature;
        private string _jsonText;
        readonly ErrorHandler _handler;
        List<ListViewFeature> list = new List<ListViewFeature>();
        readonly Dictionary<Feature, (DateTime, string)> _weatherCache = new Dictionary<Feature, (DateTime, string)>();
        MapPolyline _previousRoute;
        private static readonly Uri FilledHeart = new($@"/{Assembly.GetExecutingAssembly().GetName().Name};component\Images\FilledHeart.png", UriKind.RelativeOrAbsolute);
        private static readonly Uri HollowHeart = new Uri($@"/{Assembly.GetExecutingAssembly().GetName().Name};component\Images\HollowHeart.png", UriKind.RelativeOrAbsolute);

        public ResultsView()
        {
            _form.LoadingAnimation.Visibility = Visibility.Visible;
            _form.LoadingAnimation.IsEnabled = true;
            InitializeComponent();
            _handler = new ErrorHandler();
            
            InitializeListBox();
            _form.LoadingAnimation.Visibility = Visibility.Hidden;
            _form.LoadingAnimation.IsEnabled = false;
        }

        public Feature GetFeatureByName(string name)
        {
            foreach (Feature temp in _values.features)
            {
                if (temp.properties.name.Equals(name))
                {
                    return temp;
                }
            }
            return null;
        }

        // Calls the API to receive data about the attractions as defined according to the user
        public async void InitializeListBox()
        {
            try
            {
                //Get Json String from Lodestar api
                await Root.GetJsonFromGeoApiAsync();
            }
            catch (Exception)
            {
                Log.Error("Error with getting json from api");
                _handler.ShowError("Something went wrong. Click the Restart button and try again");
            }
            _jsonText = Root.jsonString;
            try
            {
                //Make object from json string
                _values = Root.FromJson(Root.jsonString);
            }
            catch (Exception)
            {
                Log.Error("Error is parsing json string to object...server probably returned error");
                _handler.ShowError("Something went wrong. Click the Restart button and try again");
                return;
            }



            if (_values.features == null)
            {
                Log.Error("No results");
                _handler.ShowError("No Results, Click Restart");
                return;
            }

            int unknownCounter = 1;
            int repeatCounter = 1;
            for (int i = 0; i < _values.features.Count; i++)
            {


                if (_values.features[i].properties.name == null)
                {
                    _values.features[i].properties.name = "Unamed " + _values.features[i].properties.categories[0] + " Location " + unknownCounter;
                    unknownCounter++;
                }

            }

            //Makes it so that program can differentiate by different names
            foreach (Feature f in _values.features)
            {
                var duplicates = _values.features.FindAll(e => e.properties.name.Equals(f.properties.name));
                if (duplicates.Count > 1)
                {
                    duplicates.ForEach(e =>
                    {
                        e.properties.name = e.properties.name + " #" + repeatCounter;
                        repeatCounter++;
                    });
                }
                repeatCounter = 1;
            }

            //Gets all the Weather Data asynchrounously
            List<Task<WeatherInfo.Root>> weatherInformationTasks = new List<Task<WeatherInfo.Root>>();
            _values.features.ForEach(e =>
            {
                weatherInformationTasks.Add(GetWeather(e));
            });
            await Task.WhenAll(weatherInformationTasks);

            var weatherInformation = weatherInformationTasks.Select(e => e.Result).ToList();


            var webResponse = await
                new HttpClient().GetStringAsync(
                    @$"https://touristserver.sami200.repl.co/getFavorites?cpuserialid={GetCPUSerialNumber()}");

            var response = JsonConvert.DeserializeObject<Favorites>(webResponse);

            response ??= new();
            _favorites = response.favorites.Select(e => e.placeID).ToHashSet();
            var res = weatherInformation.Zip(_values.features,
                (root, feature) => new
                {
                    Feature = feature, Weather = root,
                    Favorite = _favorites.Contains(feature.properties.place_id)? FilledHeart : HollowHeart
                }).ToList();

            
            // Cycles through the objects to place them on the map
            res.ForEach(e =>
            {
                Uri outUri;
                Uri iconUri;
                list.Add(new ListViewFeature()
                {
                    Address = e.Feature.properties.formatted,
                    Title = e.Feature.properties.name,
                    DistanceFromHome = Math.Round(e.Feature.properties.distance / 1609.34, 2) + " mi",
                    Image = Uri.TryCreate(e.Feature.properties.imgLink, UriKind.Absolute, out outUri) ? new BitmapImage(outUri) : null,
                    Temperature = e.Weather.main.temp.ToString(CultureInfo.CurrentCulture) + "F",
                    Icon = Uri.TryCreate(@"http://openweathermap.org/img/w/" + e.Weather.weather[0].icon + @".png",
                        UriKind.Absolute, out iconUri)
                        ? new BitmapImage(iconUri)
                        : null,
                    FeatureRef = e.Feature,
                    WeatherDescription = e.Weather.weather[0].description,
                    FeatureName = e.Feature.properties.name,
                    
                    FavoriteHeart = e.Favorite 
                    

                });
            });
            MainListBox.ItemsSource = null;
            MainListBox.ItemsSource = list;
            Map.Visibility = Visibility.Visible;
            AddPushPinsToMap();


            LoadingAnimation.Visibility = Visibility.Hidden;
        }

        private async Task AddRouteToMap(List<Feature> points)
        {
            //Take out any previous routes on the map
            if (_previousRoute != null)
                Map.Children.Remove(_previousRoute);

            //Convert features to SimpleWaypoints
            var waypointList = points.Select(e => new SimpleWaypoint() { Coordinate = new Coordinate(e.properties.lat, e.properties.lon) }).ToList();
            waypointList.Insert(0, new SimpleWaypoint(new Coordinate(double.Parse(Parameters.Latitude), double.Parse(Parameters.Longitude))));
            var request = new RouteRequest()
            {
                RouteOptions = new RouteOptions()
                {
                    RouteAttributes = new List<RouteAttributeType>()
                    {
                        RouteAttributeType.RoutePath
                    }
                },
                Waypoints = waypointList,
                BingMapsKey = "qc9Y7SHHRgYbev4fUy0q~ZgF6eo0fD9ieP3VnKoDX_Q~AnM4TYqGE82d-jah6trRCttSyWK53fdPmYnyOjGbcYfmD61QQYzwoRH2oNJN9AZG"

            };

            var response = await ServiceManager.GetResponseAsync(request);

            if (response != null &&
                response.ResourceSets != null &&
                response.ResourceSets.Length > 0 &&
                response.ResourceSets[0].Resources != null &&
                response.ResourceSets[0].Resources.Length > 0)
            {

                var route = response.ResourceSets[0].Resources[0] as Route;
                var coords = route.RoutePath.Line.Coordinates; //This is 2D array of lat/long values.
                var locs = new LocationCollection();
                

                for (int i = 0; i < coords.Length; i++)
                {
                    locs.Add(new Location(coords[i][0], coords[i][1]));
                }

                var routeLine = new MapPolyline()
                {
                    Locations = locs,
                    Stroke = new SolidColorBrush(Colors.SpringGreen),
                    StrokeThickness = 5,


                };

                Map.Children.Add(routeLine);
                _previousRoute = routeLine;
            }
        }

        // Adds Markers to the Bing Maps generated in the Results screen
        private void AddPushPinsToMap()
        {

            Pushpin currentLocation = new Pushpin()
            {
                Location = new Location(double.Parse(Parameters.Latitude), double.Parse(Parameters.Longitude)),
                Background = new BrushConverter().ConvertFrom("#0078d7") as SolidColorBrush,
                ToolTip = "Your Current Location\n(Might be innacurate if used Ip Address)"
            };
            currentLocation.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) =>
            {
                Map.SetView(new Location(((Pushpin)sender).Location.Latitude, ((Pushpin)sender).Location.Longitude), 15);
                var p = sender as Pushpin;
                for (var i = 0; i < MainListBox.Items.Count; i++)
                {
                    if (p.ToolTip.ToString().Equals(((ListViewFeature)MainListBox.Items[i]).Title,
                            StringComparison.CurrentCultureIgnoreCase))
                    {
                        MainListBox.SelectedIndex = i;
                    }
                }

            };
            Map.Children.Add(currentLocation);
            Map.SetView(new Location(double.Parse(Parameters.Latitude), double.Parse(Parameters.Longitude)), 15);
            foreach (Feature f in _values.features)
            {

                Pushpin pin = new Pushpin();
                var location = new Location(f.properties.lat, f.properties.lon);
                pin.Location = location;
                pin.ToolTip = f.properties.name;
                Map.Children.Add(pin);
                var startingLocation = new Location(double.Parse(Parameters.Latitude), double.Parse(Parameters.Longitude));

                pin.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) =>
                {
                    Map.SetView(new Location(((Pushpin)sender).Location.Latitude, ((Pushpin)sender).Location.Longitude), 15);
                    _selectedFeature = GetFeatureByName(((Pushpin)sender).ToolTip.ToString());
                    var p = sender as Pushpin;
                    for (int i = 0; i < MainListBox.Items.Count; i++)
                    {
                        if (p.ToolTip.ToString().Equals((MainListBox.Items[i] as ListViewFeature)?.Title,
                                StringComparison.CurrentCultureIgnoreCase))
                        {
                            MainListBox.SelectedIndex = i;
                            MainListBox.ScrollIntoView(MainListBox.SelectedItem);
                        }
                    }


                };

            }
        }


        private void MainListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MainListBox.Items.Count < 1)
            {
                Log.Debug("Got No Results From Api");
                return;
            }

            
            var listViewFeature = (MainListBox.SelectedItem as ListViewFeature);
            //31
            
            
            Clipboard.SetText(listViewFeature.FeatureRef.properties.formatted);
            new ErrorHandler().ShowError("Copied Address", color: new SolidColorBrush(Colors.SpringGreen));
            


            
            Map.SetView(new Location(listViewFeature.FeatureRef.properties.lat, listViewFeature.FeatureRef.properties.lon), 15);



        }


        // Gets weather for the place the user has selected. Helps inform the user of the current conditions in the location
        private async Task<WeatherInfo.Root> GetWeather(Feature feature)
        {






            var url = new StringBuilder(@"https://touristserver.sami200.repl.co/weather?");
            url.Append("long=");
            url.Append(feature.properties.lon);
            url.Append("&lat=");
            url.Append(feature.properties.lat);

            string outputString = await new HttpClient().GetStringAsync(url.ToString()); ;








            return JsonConvert.DeserializeObject<WeatherInfo.Root>(outputString);






        }
        //Download Button
        private async void DownloadResultsButton_Click(object sender, RoutedEventArgs e)
        {

            // create a blank Workbook object
            var workbook = new Workbook();

            // access default empty worksheet
            var worksheet = workbook.Worksheets[0];

            // set JsonLayoutOptions for formatting
            var layoutOptions = new JsonLayoutOptions()
            {
                ArrayAsTable = true
            };

            // import JSON data to CSV
            var task = Task.Run(() => JsonUtility.ImportData(_jsonText, worksheet.Cells, 0, 0, layoutOptions));


            string filePath = (System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LodestarResults", "Results[" + DateTime.Now.ToLongTimeString().Replace(":", "-").Replace(" ", "") + "].csv"));

            // Create directory temp1 if it doesn't exist
            Directory.CreateDirectory(Directory.GetParent(filePath).FullName);
            //Making sure task finished
            await task;
            // save CSV file
            workbook.Save(filePath, SaveFormat.Csv);

            //Cleans up the json File
            await Task.Run(() =>
            {
                RemoveColumnByIndex(filePath, 0);
                RemoveColumnByIndex(filePath, 0);
            });

            Clipboard.SetText(filePath);

            Process.Start(filePath);

        }

        // Cleans the data about the place to generate a human readable output report
        private void RemoveColumnByIndex(string path, int index)
        {
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                var line = reader.ReadLine();
                List<string> values = new List<string>();
                while (line != null)
                {
                    values.Clear();
                    var cols = line.Split(',');
                    for (int i = 0; i < cols.Length; i++)
                    {
                        if (i != index)
                            values.Add(cols[i]);
                    }
                    var newLine = string.Join(",", values);
                    lines.Add(newLine);
                    line = reader.ReadLine();
                }
            }

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }

        }

        //Sort list by alphabetical order
        private void AlphabeticalOrder_Click(object sender, RoutedEventArgs e)
        {
            var items = Enumerable.ToList<ListViewFeature>((IEnumerable<ListViewFeature>)MainListBox.ItemsSource).OrderBy(x => x.Title).ToList();


            MainListBox.ItemsSource = items;
        }

        //Sort list by distance from the user
        private void DistanceFromHome_Click(object sender, RoutedEventArgs e)
        {


            var items = Enumerable.ToList<ListViewFeature>((IEnumerable<ListViewFeature>)MainListBox.ItemsSource).OrderBy(x => x.DistanceFromHome).ToList();


            MainListBox.ItemsSource = items;

        }

        private async void AddToRoute_Click(object sender, RoutedEventArgs e)
        {
            //Check if there is an selected feature
            var button = sender as Button;
            var feature = GetFeatureByName(button.ToolTip.ToString());
            

            if (_routeWaypoints.Contains(feature))
            {
                _handler.ShowError("Already added to route");
                e.Handled = true;
                return;
            }

            _routeWaypoints.Add(feature);
            await AddRouteToMap(_routeWaypoints);

        }

        private async void RemoveFromRoute_Click(object sender, RoutedEventArgs e)
        {
            
            var button = sender as Button;
            var feature = GetFeatureByName(button.ToolTip.ToString());
            //Check if there is an selected feature
            
            if (!_routeWaypoints.Contains(feature))
            {
                _handler.ShowError("Already taken out");
                e.Handled = true;
                return;
            }
            _routeWaypoints.Remove(feature);
            if (_routeWaypoints.Count != 0)
                await AddRouteToMap(_routeWaypoints);
            else
            {
             Map.Children.RemoveAt(Map.Children.Count-1);
            }

        }
        private static string GetCPUSerialNumber()
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
        

        //Add Favorite
        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            var feature = GetFeatureByName(b.ToolTip.ToString());
            var client = new HttpClient();
            var addUrl = @$"https://touristserver.sami200.repl.co/addFavorite?cpuserialid={GetCPUSerialNumber()}";
            var removeUrl = @$"https://touristserver.sami200.repl.co/removeFavorite?cpuserialid={GetCPUSerialNumber()}";
            var isFavorite = false;
            await Task.Run(() => isFavorite = _favorites.Contains(feature.properties.place_id));
            if (isFavorite)
            {
                var values = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    { "placeID", feature.properties.place_id }
                });
                
                var response = await client.PostAsync(removeUrl, values);
                if (response.IsSuccessStatusCode)
                {
                    _handler.ShowError("Removed from favorites", color:new SolidColorBrush(Colors.SpringGreen));
                    b.Content = new Image() { Source = new BitmapImage(HollowHeart) };
                    _favorites.Remove(feature.properties.place_id);
                    
                }
                else
                {
                    _handler.ShowError("Error with removing from favorites");
                }
            }
            else
            {
                

                var values = new Dictionary<string, string>()
                {
                    { "address", feature.properties.formatted },
                    { "imgLink", feature.properties.imgLink },
                    { "lat", feature.properties.lat.ToString(CultureInfo.CurrentCulture) },
                    { "lon", feature.properties.lon.ToString(CultureInfo.CurrentCulture) },
                    { "name", feature.properties.name },
                    { "placeID", feature.properties.place_id }
                };
                

                var content = new FormUrlEncodedContent(values);
                
                var responseMessage = await client.PostAsync(
                    addUrl, content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    
                    b.Content = new Image()
                    {
                        Source = new BitmapImage(FilledHeart)
                        
                    };
                    _handler.ShowError("Added to favorites", color: new SolidColorBrush(Colors.SpringGreen));
                    _favorites.Add(feature.properties.place_id);
                }
                else
                {
                    _handler.ShowError("Error with adding to favorites");
                }
            }
        }
    }
}
