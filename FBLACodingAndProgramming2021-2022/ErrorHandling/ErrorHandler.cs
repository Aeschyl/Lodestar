using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FBLACodingAndProgramming2021_2022.ErrorHandling
{
    class ErrorHandler
    {
        MainWindow form = Application.Current.Windows[0] as MainWindow;
        public async void ShowError(string err)
        {
            form.ErrorTextBox.Text = err;
            form.ErrorTextBox.Opacity = 100;
            form.ErrorTextBox.Visibility = System.Windows.Visibility.Visible;
            await FadeTextBox();

            form.ErrorTextBox.Text = string.Empty;


        }

        public async Task FadeTextBox()
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