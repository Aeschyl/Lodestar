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
        
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand HaveFunSubViewCommand { get; set; }
        public RelayCommand EatSubViewCommand { get; set; }
        public RelayCommand ShopSubViewCommand { get; set; }
        public RelayCommand ExploreSubViewCommand { get; set; }
        public RelayCommand StaySubViewCommand { get; set; }
        public RelayCommand AmenitiesViewCommand { get; set; }
        public RelayCommand LocationViewCommand { get; set; }
        public RelayCommand DistanceViewCommand { get; set; }
        public RelayCommand ResultsViewCommand { get; set; }
        public RelayCommand FAQViewCommand { get; set; }
        public RelayCommand MainScreenViewCommand { get; set; }
        public RelayCommand CopyrightViewCommand { get; set; }
        public RelayCommand TermsViewCommand{ get; set; }

        public HomeViewModel HomeVM { get; set; }
        public HaveFunSubViewModel HaveFunSubVM { get; set; }
        public EatSubViewModel EatSubVM { get; set; }
        public ShopSubViewModel ShopSubVM { get; set; }
        public ExploreSubViewModel ExploreSubVM { get; set; }
        public StaySubViewModel StaySubVM { get; set; }
        public AmenitiesViewModel AmenitiesVM { get; set; }
        public LocationViewModel LocationVM { get; set; }
        public DistanceViewModel DistanceVM { get; set; }
        public ResultsViewModel ResultsVM { get; set; }
        public FAQViewModel FAQVM { get; set; }
        public MainScreenViewModel MainScreenVM { get; set; }
        public CopyrightViewModel CopyrightVM { get; set; }
        public TermsViewModel TermsVM { get; set; }

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
            AmenitiesVM = new AmenitiesViewModel();
            LocationVM = new LocationViewModel();
            DistanceVM = new DistanceViewModel();
            ResultsVM = new ResultsViewModel();
            FAQVM = new FAQViewModel();
            MainScreenVM = new MainScreenViewModel();
            CopyrightVM = new CopyrightViewModel();
            TermsVM = new TermsViewModel();

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

            AmenitiesViewCommand = new RelayCommand(o =>
            {
                CurrentView = AmenitiesVM;
            });

            LocationViewCommand = new RelayCommand(o =>
            {
                CurrentView = LocationVM;
            });

            DistanceViewCommand = new RelayCommand(o =>
            {
                CurrentView = DistanceVM;
            });

            ResultsViewCommand = new RelayCommand(o =>
            {
                CurrentView = ResultsVM;
            });

            FAQViewCommand = new RelayCommand(o =>
            {
                CurrentView = FAQVM;
            });

            MainScreenViewCommand = new RelayCommand(o => 
            {
                CurrentView = MainScreenVM;
            });

            CopyrightViewCommand = new RelayCommand(o =>
            {
                CurrentView = CopyrightVM;
            });
            TermsViewCommand = new RelayCommand(o =>
            {
                CurrentView = TermsVM;
            });
        }


    }
}
