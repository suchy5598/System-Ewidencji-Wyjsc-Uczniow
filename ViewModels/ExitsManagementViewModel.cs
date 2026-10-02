using Microsoft.EntityFrameworkCore;
using Projekt.Models;
using Projekt.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Projekt.ViewModels
{
    public class ExitsManagementViewModel : ObservableObject
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
                LoadStudents();
            }
        }

        public ICommand RefreshCommand { get; }
        public ICommand ExitCommand { get; }

        public ExitsManagementViewModel()
        {
            RefreshCommand = new RelayCommand(LoadStudents);
            ExitCommand = new RelayCommand(ExitSelected);
            LoadStudents();
            LoadClasses();
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

        public void LoadStudents()
        {
            if (selectedClass != null)
            {
                using var db = new AppDbContext();
                var studentsFromDb = db.Students.Include(s => s.SchoolClass).Where(s => s.SchoolClass == selectedClass).ToList();
                Students.Clear();
                foreach (var user in studentsFromDb)
                {
                    Students.Add(user);
                }
            }
        }

        public void ExitSelected()
        {
            if (selectedStudent != null)
            {
                //var dialog = new AddExitDialog(selectedStudent);
                //bool? result = dialog.ShowDialog();
            }
        }
    }
}
