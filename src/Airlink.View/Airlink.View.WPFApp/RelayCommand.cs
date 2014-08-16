using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Airlink.View.WPFApp
{
    // Command that relays its funcionality to other objects using delegates
    // Default for CanExecute is true
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        // Creates new command that can always execute
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        // Creates new command with execution logic and execution status logic
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        [DebuggerStepThrough]   // for the debugger to skip
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
