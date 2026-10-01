using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Projekt.ViewModels
{
    internal class AddExitDialogViewModel : ObservableObject
    {
        private DateTime exitDateTime;
        public DateTime ExitDateTime
        {
            get { return exitDateTime; }
            set
            {
                exitDateTime = value;
                OnPropertyChanged();
            }
        }

        private string reason;
        public string Reason
        {
            get { return reason; }
            set
            {
                reason = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand;
        public ICommand ExitCommand;

        public AddExitDialogViewModel()
        {
            ExitDateTime = DateTime.Now;
            Reason = "";
            AddCommand = new RelayCommand(Add);
            ExitCommand = new RelayCommand(Exit);
        }

        public void Add()
        {

        }

        public void Exit()
        {

        }

        // dodać drugi dialog z edycją
    }
}
