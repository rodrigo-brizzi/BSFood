using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BSFoodPDV.Apoio
{
    public class DelegateCommand : ICommand
    {
        #region Fields
 
        private readonly Action<object> _execute = null;
        private readonly Predicate<object> _canExecute = null;
 
        #endregion
 
        #region Constructors
 
        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public DelegateCommand(Action<object> execute) 
            : this(execute, null)
        {
        }
 
        /// <summary>
        /// Creates a new command with conditional execution.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
 
            _execute = execute;
            _canExecute = canExecute;
        }
 
        #endregion
 
        #region ICommand Members
 
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }
 
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }
 
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
 
        #endregion
    }
}
