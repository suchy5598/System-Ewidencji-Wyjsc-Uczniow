using Microsoft.EntityFrameworkCore;
using Projekt.Models;
using Projekt.Windows;
using System.Collections.ObjectModel;
using System.Windows;
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
        public ICommand ReturnCommand { get; }

        public ExitsManagementViewModel()
        {
            RefreshCommand = new RelayCommand(LoadStudents);
            ExitCommand = new RelayCommand(ExitSelected, () => selectedStudent != null && selectedStudent.IsInClass);
            ReturnCommand = new RelayCommand(ReturnSelected, () => selectedStudent != null && !selectedStudent.IsInClass);
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
            if (selectedStudent != null && selectedStudent.IsInClass)
            {
                // 1. Podpinamy nasz ViewModel
                var exitVm = new AddExitViewModel(SelectedStudent);

                // 2. Otwieramy to samo uniwersalne okno EditDialogWindow
                var dialog = new EditDialog
                {
                    DataContext = exitVm,
                    Owner = Application.Current.MainWindow
                };

                if (dialog.ShowDialog() == true)
                {
                    LoadStudents();
                }
            }
        }

        public void ReturnSelected()
        {
            if (selectedStudent != null && !selectedStudent.IsInClass)
            {
                using var db = new AppDbContext();
                var activeExit = db.Exits.FirstOrDefault(e => e.StudentId == selectedStudent.Id && e.ReturnTime == default);

                if (activeExit == null) return; 

                // 1. Podpinamy nasz ViewModel
                var exitVm = new EndExitViewModel(SelectedStudent, activeExit.ExitTime, activeExit.Reason);

                // 2. Otwieramy to samo uniwersalne okno EditDialogWindow
                var dialog = new EditDialog
                {
                    DataContext = exitVm,
                    Owner = Application.Current.MainWindow
                };

                if (dialog.ShowDialog() == true)
                {
                    LoadStudents();
                }
            }
        }
    }
}
