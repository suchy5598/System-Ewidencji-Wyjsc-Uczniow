namespace Projekt.ViewModels
{
    public abstract class DialogViewModelBase : ObservableObject
    {
        private bool? _dialogResult;
        public bool? DialogResult
        {
            get => _dialogResult;
            set
            {
                _dialogResult = value;
                OnPropertyChanged();
            }
        }

        protected void CloseDialog(bool result)
        {
            DialogResult = result;
        }
    }
}
