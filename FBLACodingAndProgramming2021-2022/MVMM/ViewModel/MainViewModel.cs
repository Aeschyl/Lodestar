using FBLACodingAndProgramming2021_2022.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBLACodingAndProgramming2021_2022.MVMM.ViewModel
{

    class MainViewModel : ObservableObject
    {

        public HomeViewModel HomeVM { get; set; }
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
               //I Commented this code out and then It started to work and I have no clue why
               //OnPropertyChanged()
            
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CurrentView = HomeVM;
        }


    }
}
