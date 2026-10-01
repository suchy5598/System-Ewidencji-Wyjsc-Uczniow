using Projekt.Models;
using System.Data;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Input;

namespace Projekt.ViewModels
{
    public class EditAccountViewModel : DialogViewModelBase
    {
        public User UserToEdit { get; set; }

        private string login = string.Empty;
        public string Login
        {
            get => login;
            set { login = value; OnPropertyChanged(); }
        }

        // Opcjonalne pole do zmiany hasła (jeśli puste -> nie zmieniamy hasła)
        private string newPassword = string.Empty;
        public string NewPassword
        {
            get => newPassword;
            set { newPassword = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public EditAccountViewModel(User user)
        {
            UserToEdit = user;
            login = user.Login;
            SaveCommand = new RelayCommand(SaveData);
            CancelCommand = new RelayCommand(Cancel);
        }

        public void SaveData()
        {
            if (string.IsNullOrWhiteSpace(Login))
            {
                MessageBox.Show("Login nie może być pusty!");
                return;
            }

            using var db = new AppDbContext();

            // 1. Pobieramy użytkownika z bazy po jego ID
            var userInDb = db.Users.FirstOrDefault(u => u.Id == UserToEdit.Id);
            if (userInDb != null)
            {
                // Sprawdzamy czy zmiana loginu nie koliduje z innym użytkownikiem
                if (userInDb.Login != Login && db.Users.Any(u => u.Login.ToLower() == Login.ToLower()))
                {
                    MessageBox.Show("Użytkownik o podanym loginie już istnieje!");
                    return;
                }

                userInDb.Login = Login;

                // Zmiana hasła tylko jeśli wpisano nowe
                if (!string.IsNullOrWhiteSpace(NewPassword))
                {
                    userInDb.PasswordHash = BCrypt.Net.BCrypt.HashPassword(NewPassword);
                }

                Debug.WriteLine("test");

                db.SaveChanges();
                CloseDialog(true);
            }
        }

        private void Cancel() // Metoda BEZ argumentów
        {
            CloseDialog(false);
        }
    }
}
