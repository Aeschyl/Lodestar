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

namespace FBLACodingAndProgramming2021_2022.ErrorHandling
{

    
    class ErrorHandler
    {
        private bool isBusy;
        
        
        
        
        //Same Error Showing but stays on the screen
        public async void ShowError(string err, bool stay = false)
        {
            if (isBusy)
            {
                return;
            }
            isBusy = true;
            await new ErrorShower(err, stay).ShowError();
            isBusy = false;



        }

        

        

        

       


    }

    class ErrorShower
    {
        public string err { get; set; }
        public bool stay { get; set; }
        public ErrorShower(string err, bool stay)
        {
            this.err = err;
            this.stay = stay;
        }
        // Overloaded methods to display the error box with the error that has occured
        
        //Same Error Showing but stays on the screen
        public async Task ShowError()
        {
            MainWindow form = Application.Current.Windows[0] as MainWindow;
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
