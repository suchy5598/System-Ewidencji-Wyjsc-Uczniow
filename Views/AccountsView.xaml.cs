using Projekt.ViewModels;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Projekt.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AccountsView.xaml
    /// </summary>
    public partial class AccountsView : UserControl
    {
        public AccountsView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(UserSession.LoggedIn);
            Debug.WriteLine(UserSession.IsAdmin);
            if (UserSession.LoggedIn && UserSession.IsAdmin)
            {
                string login = LoginBox.Text.Trim();
                string pass = PasswordBox.Text;
                if (string.IsNullOrEmpty(login))
                {
                    MessageBox.Show("Login nie może być pusty", "Bląd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(pass))
                {
                    MessageBox.Show("Hasło nie może być puste", "Bląd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string hash = BCrypt.Net.BCrypt.HashPassword(pass);
                using (var db = new AppDbContext())
                {
                    if (db.Users.Any(u => u.Login == login))
                    {
                        MessageBox.Show("Użytkownik z danym loginem już istnieje", "Bląd", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    db.Users.Add(new()
                    {
                        Login = login,
                        PasswordHash = hash,
                        UserType = Models.UserType.Teacher
                    });
                    db.SaveChanges();

                    LoginBox.Clear();
                    PasswordBox.Clear();

                    if (DataContext is AccountsViewModel avm)
                    {
                        avm.LoadUsers();
                    }

                    MessageBox.Show("Konto zostało stworzone", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
