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
        public RelayCommand ShopSubViewCommand { get; set; }
        public RelayCommand ExploreSubViewCommand { get; set; }
        public RelayCommand StaySubViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public HaveFunSubViewModel HaveFunSubVM { get; set; }
        public EatSubViewModel EatSubVM { get; set; }
        public ShopSubViewModel ShopSubVM { get; set; }
        public ExploreSubViewModel ExploreSubVM { get; set; }
        public StaySubViewModel StaySubVM { get; set; }

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
            ShopSubVM = new ShopSubViewModel();
            ExploreSubVM = new ExploreSubViewModel();
            StaySubVM = new StaySubViewModel();
            


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

            ShopSubViewCommand = new RelayCommand(o =>
            {
                CurrentView = ShopSubVM;
            });

            ExploreSubViewCommand = new RelayCommand(o =>
            {
                CurrentView = ExploreSubVM;
            });

            StaySubViewCommand = new RelayCommand(o =>
            {
                CurrentView = StaySubVM;
            });



        }


    }
}
