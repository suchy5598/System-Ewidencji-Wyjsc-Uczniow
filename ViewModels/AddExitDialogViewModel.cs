using Projekt.Models;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Projekt.ViewModels
{
    internal class AddExitDialogViewModel : DialogViewModelBase
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

        public ICommand AddCommand;
        public ICommand ExitCommand;

        public AddExitDialogViewModel(Student selectedStudent)
        {
            student = selectedStudent;
            ExitDateTime = DateTime.Now;
            Reason = "";
            AddCommand = new RelayCommand(Add);
            ExitCommand = new RelayCommand(Exit);
        }

        public void Add()
        {
            if (student.IsInClass)
            {
                using var db = new AppDbContext();
                student.IsInClass = false;
                db.Exits.Add(new()
                {
                    ExitTime = ExitDateTime,
                    StudentId = student.Id,
                    Reason = reason,
                });
                db.SaveChanges();
                CloseDialog(true);
            }
        }

        public void Exit()
        {
            CloseDialog(false);
        }

        // dodać drugi dialog z edycją
    }
}
