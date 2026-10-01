using Projekt.Models;
using Projekt.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Projekt.ViewModels
{
    public class AccountsViewModel : ObservableObject
    {
        public ObservableCollection<User> Users { get; set; } = [];

        private User selectedUser;
        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public AccountsViewModel()
        {
            RefreshCommand = new RelayCommand(LoadUsers);
            DeleteCommand = new RelayCommand(DeleteSelected);
            EditCommand = new RelayCommand(EditSelected);
            LoadUsers();
        }

        public void LoadUsers()
        {
            using var db = new AppDbContext();
            var usersFromDb = db.Users.ToList();
            Users.Clear();
            foreach (var user in usersFromDb)
            {
                Users.Add(user);
            }
        }

        public void DeleteSelected()
        {
            if (selectedUser != null)
            {
                var result = MessageBox.Show($"Usunąć konto {selectedUser.Login}?", "Usunięcie konta", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    using var db = new AppDbContext();
                    db.Users.Remove(selectedUser);
                    db.SaveChanges();
                    MessageBox.Show($"Konto {selectedUser.Login} zostało usunięte", "Usunięcie konta", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadUsers();
                }
            }
        }

        public void EditSelected()
        {
            if (selectedUser != null)
            {
                var editVm = new EditAccountViewModel(selectedUser);
                var dialog = new EditDialog()
                {
                    DataContext = editVm,
                    Owner = Application.Current.MainWindow
                };

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    LoadUsers();
                }
            }
        }
    }
}
