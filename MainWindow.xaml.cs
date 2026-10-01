using Projekt.ViewModels;
using System.Windows;

namespace Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Ustawiamy Główny ViewModel jako Kontext Danych dla Okna
            DataContext = new MainViewModel();
        }
    }
}