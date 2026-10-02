using Projekt.Models;
using System.Windows;
using System.Windows.Input;

namespace Projekt.ViewModels
{
    internal class EndExitViewModel : DialogViewModelBase
    {
        private Student student;

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

        private DateTime returnDateTime;
        public DateTime ReturnDateTime
        {
            get { return returnDateTime; }
            set
            {
                returnDateTime = value;
                OnPropertyChanged();
            }
        }

        public ICommand EndCommand { get; }
        public ICommand ExitCommand { get; }

        public EndExitViewModel(Student selectedStudent, DateTime exitTime, string reason)
        {
            student = selectedStudent;
            ExitDateTime = exitTime;
            Reason = reason;
            ReturnDateTime = DateTime.Now;
            EndCommand = new RelayCommand(EndExit, CanMarkEnd);
            ExitCommand = new RelayCommand(Exit);
        }
        
        private void EndExit()
        {
            if (student != null)
            {
                if (returnDateTime < exitDateTime)
                {
                    MessageBox.Show("Czas powrotu jest niepoprawny");
                    return;
                }

                using var db = new AppDbContext();
                var activeExit = db.Exits.FirstOrDefault(e => e.StudentId == student.Id && e.ReturnTime == default);

                if (activeExit != null)
                {
                    activeExit.ReturnTime = returnDateTime;
                }

                var studentInDb = db.Students.FirstOrDefault(s => s.Id == student.Id);
                if (studentInDb != null)
                {
                    studentInDb.IsInClass = true;
                }

                db.SaveChanges();
                CloseDialog(true);
            }
        }

        private void Exit()
        {
            CloseDialog(false);
        }

        private bool CanMarkEnd()
        {
            return returnDateTime > exitDateTime;
        }
    }
}
