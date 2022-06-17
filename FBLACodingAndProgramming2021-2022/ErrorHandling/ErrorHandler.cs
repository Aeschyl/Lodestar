/*
    A class to smoothly handle any errors that might occur for the user
    Imposes a error message on top of the screen to notify the error that has occured
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using log4net.Appender;

namespace FBLACodingAndProgramming2021_2022.ErrorHandling
{

    
    class ErrorHandler
    {
        private bool isBusy;
        private Brush _defaultColor = new SolidColorBrush(Colors.Red);
        
        
        
        //Same Error Showing but stays on the screen
        public async void ShowError(string err, bool stay = false, Brush color = null)
        {
            if (isBusy)
            {
                return;
            }
            isBusy = true;
            color ??= _defaultColor;
            await new ErrorShower(err, stay, color).ShowError();
            isBusy = false;



        }

        

        

        

       


    }

    internal class ErrorShower
    {
        public string err { get; set; }
        public bool stay { get; set; }
        public Brush brush { get; set; }
        public ErrorShower(string err, bool stay, Brush color = null)
        {
            this.err = err;
            this.stay = stay;
            brush = color ?? new SolidColorBrush(Colors.Red);
        }
        // Overloaded methods to display the error box with the error that has occured
        
        //Same Error Showing but stays on the screen
        public async Task ShowError()
        {
            MainWindow form = Application.Current.Windows[0] as MainWindow;
            form.ErrorTextBox.Background = brush;
            form.ErrorTextBox.Text = err;
            form.ErrorTextBox.Opacity = 100;
            form.ErrorTextBox.Visibility = System.Windows.Visibility.Visible;
            if (!stay)
            {
                await FadeTextBox(form);
                form.ErrorTextBox.Text = string.Empty;
            }
            

        }



        public async Task FadeTextBox(MainWindow form)
        {
            await Task.Delay(1000); // Wait 1 seconds

            for (int i = 99; i >= 0; i--)
            {
                form.ErrorTextBox.Opacity = i / 100d;

                await Task.Delay(15); // The animation will take 1.5 seconds
            }

            form.ErrorTextBox.Visibility = System.Windows.Visibility.Hidden;


            form.ErrorTextBox.Opacity = 0;
        }
    }
}
