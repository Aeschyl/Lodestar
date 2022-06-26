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
       
        public FavoritesView()
        {
            InitializeComponent();
        }
    }
}
