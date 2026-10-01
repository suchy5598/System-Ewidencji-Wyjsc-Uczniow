using Projekt.Models;
using Projekt.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Projekt.ViewModels
{
    public class ClassesViewModel : ObservableObject
    {
        public ObservableCollection<SchoolClass> Classes { get; set; } = [];

        private SchoolClass selectedClass;
        public SchoolClass SelectedClass
        {
            get => selectedClass;
            set
            {
                selectedClass = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public ClassesViewModel()
        {
            RefreshCommand = new RelayCommand(LoadClasses);
            DeleteCommand = new RelayCommand(DeleteSelected);
            EditCommand = new RelayCommand(EditSelected);
            LoadClasses();
        }

        public void LoadClasses()
        {
            using var db = new AppDbContext();
            var classesFromDb = db.Classes.ToList();
            Classes.Clear();
            foreach (var cls in classesFromDb)
            {
                Classes.Add(cls);
            }
        }

        public void DeleteSelected()
        {
            if (selectedClass != null)
            {
                var result = MessageBox.Show($"Usunąć klasę {selectedClass.Name}?", "Usunięcie klasy", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    using var db = new AppDbContext();
                    db.Classes.Remove(selectedClass);
                    db.SaveChanges();
                    MessageBox.Show($"Klasa {selectedClass.Name} została usunięta", "Usunięcie klasy", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadClasses();
                }
            }
        }

        public void EditSelected()
        {
            if (selectedClass != null)
            {
                var editVm = new EditClassViewModel(selectedClass);
                var dialog = new EditDialog()
                {
                    DataContext = editVm,
                    Owner = Application.Current.MainWindow
                };

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    LoadClasses();
                }
            }
        }
    }
}
