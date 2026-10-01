using Projekt.Models;
using System.Windows;
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
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Nazwa nie może być pusta", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using var db = new AppDbContext();

            var classInDb = db.Classes.FirstOrDefault(c => c.Id == ClassToEdit.Id);
            if (classInDb != null)
            {
                // Sprawdzamy czy zmiana loginu nie koliduje z innym użytkownikiem
                if (classInDb.Name != Name && db.Classes.Any(c => c.Name.ToLower() == name.ToLower()))
                {
                    MessageBox.Show("Użytkownik o podanym loginie już istnieje!");
                    return;
                }

                classInDb.Name = name;
                db.SaveChanges();

                CloseDialog(true);
            }

        }

        public void Cancel()
        {
            CloseDialog(false);
        }
    }
}
