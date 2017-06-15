using System;
using System.Diagnostics;
using System.Windows.Input;

namespace RsaAnalyzer.ViewModels
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. The default return value for the
    /// CanExecute method is 'true'.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        private EventHandler _internalCanExecuteChanged;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, 
            Predicate<object> canExecute)
        {
            _execute = execute ?? 
                throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                _internalCanExecuteChanged += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                _internalCanExecuteChanged -= value;
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }


        /// <summary>
        /// This method can be used to raise the CanExecuteChanged handler.
        /// This will force WPF to re-query the status of this command directly.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (_canExecute != null)
                OnCanExecuteChanged();
        }

        /// <summary>
        /// This method is used to walk the delegate chain and well WPF that
        /// our command execution status has changed.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            var eCanExecuteChanged = _internalCanExecuteChanged;
            eCanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion // ICommand Members
    }

}
