using Projekt.Models;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Projekt.ViewModels
{
    internal class AddExitViewModel : DialogViewModelBase
    {
        private Student student;

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

        public ICommand AddCommand { get; }
        public ICommand ExitCommand { get; }

        public AddExitViewModel(Student selectedStudent)
        {
            student = selectedStudent;
            ExitDateTime = DateTime.Now;
            Reason = "";
            AddCommand = new RelayCommand(Save);
            ExitCommand = new RelayCommand(Cancel);
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Reason))
            {
                MessageBox.Show("Proszę podać powód wyjścia!");
                return;
            }

            using var db = new AppDbContext();

            // 1. Tworzymy nowy wpis wyjścia
            var newExit = new Exit
            {
                StudentId = student.Id,
                UserId = UserSession.CurrentUser.Id,
                ExitTime = DateTime.Now,
                Reason = Reason
            };

            db.Exits.Add(newExit);

            // 2. Aktualizujemy status ucznia w bazie (IsOutInClass -> false)
            var studentInDb = db.Students.FirstOrDefault(s => s.Id == student.Id);
            if (studentInDb != null)
            {
                studentInDb.IsInClass = false;
            }

            db.SaveChanges();

            // 3. Zamykamy dialog z wynikiem sukcesu
            CloseDialog(true);
        }

        private void Cancel()
        {
            CloseDialog(false);
        }

        // dodać drugi dialog z edycją
    }
}
