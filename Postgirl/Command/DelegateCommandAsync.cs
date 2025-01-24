using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Postgirl.Command;

public class DelegateCommandAsync : ICommand
{
    private readonly Func<object?, Task> _execute;
    private readonly Func<object?, bool>? _canExecute;

    public DelegateCommandAsync(Func<object?, Task> execute, Func<object?, bool>? canExecute = null)
    {
        ArgumentNullException.ThrowIfNull(execute);
        _execute = execute;
        _canExecute = canExecute;
    }

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => _canExecute is null || _canExecute(parameter);

    public async void Execute(object? parameter)
    {
        try
        {
            await _execute(parameter);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

    }
}