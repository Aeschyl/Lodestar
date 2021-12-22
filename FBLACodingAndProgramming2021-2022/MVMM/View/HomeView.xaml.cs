using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        public HomeView()
        {
            InitializeComponent();
        }

        private void EatingButton_Click(object sender, RoutedEventArgs e)
        {
            var param = new Parameters();
            param.AddAmenities("Restroom");
            param.Serialize();
            Thread.Sleep(4);
            var obj = param.DeSerialize();
            MessageBox.Show(obj.ToString());

        }
        //Have Fun Button
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

            //Automation Techinique to activate a button on the main window to chaneg the views since I was getting errors when trying to swtich views insde the Home View
            ButtonAutomationPeer peer = new ButtonAutomationPeer(Form.HaveFunActivator);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();

        }
    }
}
