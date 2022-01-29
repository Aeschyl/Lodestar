/*
    The file that enables the hyperlinks on the copyright screen to open in a browser
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    /// Interaction logic for CopyrightView.xaml
    /// </summary>
    public partial class CopyrightView : UserControl
    {
        public CopyrightView()
        {
            InitializeComponent();
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
        private void ContactUs(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStart("mailto:help.lodestar@gmail.com"));
            e.Handled = true;
        }
    }
}
