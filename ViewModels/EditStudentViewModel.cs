using Projekt.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Projekt.ViewModels
{
    public class EditStudentViewModel : DialogViewModelBase
    {
        public ObservableCollection<SchoolClass> Classes { get; set; } = [];
        public Student StudentToEdit { get; set; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private string name = string.Empty;
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        private string surname = string.Empty;
        public string Surname
        {
            get => surname;
            set { surname = value; OnPropertyChanged(); }
        }

        private SchoolClass schoolClass;
        public SchoolClass Class
        {
            get => schoolClass;
            set { schoolClass = value; OnPropertyChanged(); }
        }

        public EditStudentViewModel(Student s)
        {
            LoadClasses();
            StudentToEdit = s;
            Name = s.Name;
            Surname = s.Surname;
            if (s.SchoolClass != null)
            {
                Class = Classes.FirstOrDefault(c => c.Id == s.SchoolClass.Id);
            }
            SaveCommand = new RelayCommand(SaveData);
            CancelCommand = new RelayCommand(Cancel);
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


        public void SaveData()
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Imię nie może być puste", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(surname))
            {
                MessageBox.Show("Nazwisko nie może być puste", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (schoolClass == null)
            {
                MessageBox.Show("Klasa nie może być pusta", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using var db = new AppDbContext();
            var studentInDb = db.Students.FirstOrDefault(s => s.Id == StudentToEdit.Id);
            if (studentInDb != null)
            {
                studentInDb.Name = name;
                studentInDb.Surname = surname;
                studentInDb.SchoolClassId = schoolClass.Id;
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
