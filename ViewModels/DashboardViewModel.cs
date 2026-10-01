using System.Windows.Input;

namespace Projekt.ViewModels
{
    public class DashboardViewModel : ObservableObject
    {
        private string userName = string.Empty;
        private object _currentDashboardView;
        public object CurrentDashboardView
        {
            get => _currentDashboardView;
            set
            {
                _currentDashboardView = value;
                OnPropertyChanged();
            }
        }

        public ExitsManagementViewModel ExitsVm { get; } = new();
        public StatisticsViewModel StatisticsVm { get; } = new();
        public AccountsViewModel AccountsVm { get; } = new();
        public ClassesViewModel ClassesVm { get; } = new();
        public StudentsViewModel StudentsVm { get; } = new();

        public ICommand ShowExitsCommand { get; }
        public ICommand ShowStatisticsCommand { get; }
        public ICommand ShowAccountsCommand { get; }
        public ICommand ShowStudentsCommand { get; }
        public ICommand ShowClassesCommand { get; }

        public bool IsAdmin => UserSession.CurrentUser?.UserType == Models.UserType.Admin;

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }

        public DashboardViewModel()
        {
            if (UserSession.LoggedIn)
            {
                UserName = UserSession.CurrentUser?.Login ?? "user";
            }
            ShowExitsCommand = new RelayCommand(() => CurrentDashboardView = ExitsVm);
            ShowStatisticsCommand = new RelayCommand(() => CurrentDashboardView = StatisticsVm);
            ShowAccountsCommand = new RelayCommand(() => CurrentDashboardView = AccountsVm);
            ShowClassesCommand = new RelayCommand(() => CurrentDashboardView = ClassesVm);
            ShowStudentsCommand = new RelayCommand(() => CurrentDashboardView = StudentsVm);
        }
    }
}
