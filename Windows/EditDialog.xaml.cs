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
using System.Windows.Shapes;

namespace Projekt.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy EditDialog.xaml
    /// </summary>
    public partial class EditDialog : Window
    {
        public EditDialog()
        {
            InitializeComponent();

            DataContextChanged += (s, e) =>
            {
                if (DataContext is DialogViewModelBase vm)
                {
                    vm.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(vm.DialogResult) && vm.DialogResult.HasValue)
                        {
                            DialogResult = vm.DialogResult;
                            Close();
                        }
                    };
                }
            };
        }
    }
}
