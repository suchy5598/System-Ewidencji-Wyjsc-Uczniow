using System;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    private readonly Action action;
    private readonly Func<bool> canExecute;
    public RelayCommand(Action execute) => action = execute;
    public RelayCommand(Action execute, Func<bool> canExecute)
    {
        action = execute;
        this.canExecute = canExecute;
    }

    public bool CanExecute(object parameter) => canExecute?.Invoke() ?? true;
    public void Execute(object parameter) => action();
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}