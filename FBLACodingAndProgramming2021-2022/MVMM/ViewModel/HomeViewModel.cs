using FBLACodingAndProgramming2021_2022.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBLACodingAndProgramming2021_2022.MVMM.ViewModel
{
    class HomeViewModel:ObservableObject
    {
        
        public RelayCommand HaveFunSubViewCommand { get; set; }

        
        public HaveFunSubViewModel HaveFunSubVM { get; set; }

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

        public HomeViewModel()
        {
            
            HaveFunSubVM = new HaveFunSubViewModel();





           

            HaveFunSubViewCommand = new RelayCommand(o =>
            {
                CurrentView = HaveFunSubVM;
            });




        }

    }
}
