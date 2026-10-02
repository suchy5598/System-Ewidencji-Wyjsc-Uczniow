using Projekt.ViewModels;
using System.Windows;

namespace Projekt.Windows
{
    public partial class AddExitDialog : Window
    {
        public AddExitDialog()
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
