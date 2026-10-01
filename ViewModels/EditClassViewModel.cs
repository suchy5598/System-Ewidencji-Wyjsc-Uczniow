using Projekt.Models;
using System.Windows.Input;

namespace Projekt.ViewModels
{
    public class EditClassViewModel : DialogViewModelBase
    {
        public SchoolClass ClassToEdit { get; set; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private string name = string.Empty;
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public EditClassViewModel(SchoolClass cls)
        {
            ClassToEdit = cls;
            Name = cls.Name;
            SaveCommand = new RelayCommand(SaveData);
            CancelCommand = new RelayCommand(Cancel);
        }

        public void SaveData()
        {
            CloseDialog(true);
        }

        public void Cancel()
        {
            CloseDialog(false);
        }
    }
}
