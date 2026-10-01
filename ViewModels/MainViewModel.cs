using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projekt.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(); // Informuje WPF o zmianie widoku
            }
        }

        // Komendy przełączające
        public ICommand ShowLoginCommand { get; }
        public ICommand ShowDashboardCommand { get; }

        public MainViewModel()
        {
            // Domyślny widok po starcie
            CurrentView = new LoginViewModel();

            // Inicjalizacja komend
            ShowLoginCommand = new RelayCommand(() => CurrentView = new LoginViewModel());
            ShowDashboardCommand = new RelayCommand(() => CurrentView = new DashboardViewModel());
        }
    }
}
