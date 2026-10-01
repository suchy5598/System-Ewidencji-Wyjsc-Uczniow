using Projekt.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Projekt.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Login()
        {
            string login = LoginBox.Text.Trim();
            string password = PassBox.Password;
            if (string.IsNullOrEmpty(login) ||
                string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Wprowadź login i hasło", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new AppDbContext())
            {
                bool success = false;
                var user = db.Users.FirstOrDefault(u => u.Login == login);
                if (user != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                    {
                        if (DataContext is LoginViewModel lvm &&
                            Application.Current.MainWindow?.DataContext is MainViewModel mvm)
                        {
                            UserSession.Login(user);
                            mvm.CurrentView = new DashboardViewModel();
                            success = true;
                        }
                    }
                }

                if (!success)
                {
                    MessageBox.Show("Niepoprawny login lub hasło", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void LoginBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                PassBox.Focus();
                e.Handled = true;
            }
        }

        private void PassBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                Login();
                e.Handled = true;
            }
        }
    }
}
