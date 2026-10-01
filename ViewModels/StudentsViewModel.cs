using Microsoft.EntityFrameworkCore;
using Projekt.Models;
using Projekt.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Projekt.ViewModels
{
    public class StudentsViewModel : ObservableObject
    {
        public ObservableCollection<Student> Students { get; set; } = [];
        public ObservableCollection<SchoolClass> Classes { get; set; } = new();

        private Student selectedStudent;
        public Student SelectedStudent
        {
            get => selectedStudent;
            set
            {
                selectedStudent = value;
                OnPropertyChanged();
            }
        }

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

        public StudentsViewModel()
        {
            RefreshCommand = new RelayCommand(LoadStudents);
            DeleteCommand = new RelayCommand(DeleteSelected);
            EditCommand = new RelayCommand(EditSelected);
            LoadStudents();
            LoadClasses();
        }

        public void LoadStudents()
        {
            using var db = new AppDbContext();
            var studentsFromDb = db.Students.Include(s => s.SchoolClass).ToList();
            Students.Clear();
            foreach (var user in studentsFromDb)
            {
                Students.Add(user);
            }
        }

        public void LoadClasses()
        {
            using var db = new AppDbContext();
            var table = db.Classes.ToList();
            Classes.Clear();
            foreach (var cls in table)
            {
                Classes.Add(cls);
            }
        }

        public void DeleteSelected()
        {
            if (selectedStudent != null)
            {
                var result = MessageBox.Show($"Usunąć ucznia {selectedStudent.Name} {selectedStudent.Surname}?", "Usunięcie ucznia", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    using var db = new AppDbContext();
                    db.Students.Remove(selectedStudent);
                    db.SaveChanges();
                    MessageBox.Show($"Uczeń {selectedStudent.Name} {selectedStudent.Surname} został usunięty", "Usunięcie ucznia", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadStudents();
                }
            }
        }

        public void EditSelected()
        {
            if (selectedStudent != null)
            {
                var editVm = new EditStudentViewModel(selectedStudent);
                var dialog = new EditDialog()
                {
                    DataContext = editVm,
                    Owner = Application.Current.MainWindow
                };

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    LoadStudents();
                }
            }
        }
    }
}
