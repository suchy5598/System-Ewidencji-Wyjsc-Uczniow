using Projekt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ClassesView.xaml
    /// </summary>
    public partial class ClassesView : UserControl
    {
        public ClassesView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserSession.LoggedIn && UserSession.IsAdmin)
            {
                string name = NameBox.Text.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Nazwa nie może być pusta", "Bląd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var db = new AppDbContext())
                {
                    if (db.Classes.Any(u => u.Name == name))
                    {
                        MessageBox.Show("Klasa z daną nazwą już istnieje", "Bląd", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    db.Classes.Add(new()
                    {
                        Name = name
                    });
                    db.SaveChanges();

                    NameBox.Clear();

                    if (DataContext is ClassesViewModel cvm)
                    {
                        cvm.LoadClasses();
                    }

                    MessageBox.Show("Klasa została stworzona", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
