using Projekt.Models;
using Projekt.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logika interakcji dla klasy StudentsView.xaml
    /// </summary>
    public partial class StudentsView : UserControl
    {
        public StudentsView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserSession.LoggedIn && UserSession.IsAdmin)
            {
                string name = NameBox.Text.Trim();
                string surname = SurnameBox.Text.Trim();

                SchoolClass cls = null;
                if (DataContext is StudentsViewModel svm)
                {
                    cls = svm.SelectedClass;
                    Debug.WriteLine(svm.SelectedClass == null);
                }

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Imię nie może być puste", "Bląd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(surname))
                {
                    MessageBox.Show("Nazwisko nie może być puste", "Bląd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (cls == null)
                {
                    MessageBox.Show("Klasa nie może być pusta", "Bląd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var db = new AppDbContext())
                {
                    db.Students.Add(new()
                    {
                        Name = name,
                        Surname = surname,
                        SchoolClassId = cls.Id
                    });
                    db.SaveChanges();

                    NameBox.Clear();
                    SurnameBox.Clear();
                    ClassBox.SelectedItem = null;

                    if (DataContext is StudentsViewModel svm2)
                    {
                        svm2.LoadStudents();
                    }

                    MessageBox.Show("Uczeń został dodany", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
