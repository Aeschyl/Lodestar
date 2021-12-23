﻿using FBLACodingAndProgramming2021_2022.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBLACodingAndProgramming2021_2022.MVMM.ViewModel
{

    class MainViewModel : ObservableObject
    {
        
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand HaveFunSubViewCommand { get; set; }
        public RelayCommand EatSubViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public HaveFunSubViewModel HaveFunSubVM { get; set; }
        public EatSubViewModel EatSubVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;

                OnPropertyChanged();
            
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            HaveFunSubVM = new HaveFunSubViewModel();
            EatSubVM = new EatSubViewModel();

            


            HomeViewCommand = new RelayCommand(o => 
            {
                CurrentView = HomeVM;
            });

            HaveFunSubViewCommand = new RelayCommand(o =>
            {
                CurrentView = HaveFunSubVM;
            });

            EatSubViewCommand = new RelayCommand(o => 
            {
                CurrentView = EatSubVM;
            });

            

           
        }


    }
}
