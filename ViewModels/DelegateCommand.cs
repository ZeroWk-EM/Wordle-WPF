using System;
using System.Windows.Input;

namespace WordleWPF.ViewModels
{
    public class DelegateCommand : ICommand
    {
        #region Variabili
        private Action<object> _execute;
        Predicate<object>? _canExecute;
        public event EventHandler? CanExecuteChanged;
        #endregion

        #region Costruttore
        public DelegateCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        #endregion

        #region Metodi
        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke(parameter) != false;
        }

        public void Execute(object? parameter)
        {
            _execute?.Invoke(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
