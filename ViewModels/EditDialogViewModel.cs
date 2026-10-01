using Projekt.Views;
using System.Windows.Input;

namespace Projekt.ViewModels
{
    public class EditDialogViewModel : ObservableObject
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
        public ICommand ShowClassCommand { get; }
        public ICommand ShowStudentCommand { get; }
        public ICommand ShowAccountCommand { get; }

        public EditDialogViewModel()
        {
            // Domyślny widok po starcie
            CurrentView = new EditClassView();

            // Inicjalizacja komend
            ShowClassCommand = new RelayCommand(() => CurrentView = new EditClassView());
            ShowStudentCommand = new RelayCommand(() => CurrentView = new EditStudentView());
            ShowAccountCommand = new RelayCommand(() => CurrentView = new EditAccountView());
        }
    }
}
