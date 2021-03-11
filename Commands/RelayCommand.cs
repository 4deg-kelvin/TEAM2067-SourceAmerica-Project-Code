using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TEAM2067_SourceAmerica_Project.Commands
{
    /// <summary>
    /// Provides a class for relaying commands to prevent having to make a command for each class
    /// </summary>
   
    public class RelayCommand : ICommand
    {
        readonly Action<object> _action;
        readonly Predicate<object> _canexecute;

        public RelayCommand(Action<object> action, Predicate<object> canExecute)
        {
            if (action == null)
                throw new NullReferenceException();

            _action = action;
            _canexecute = canExecute;

        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }

        public bool CanExecute(object parameter)
        {
            //Predicate holds the canexecute method decided by the viewmodel
            //if null simply means that no data needed to be passed
            return _canexecute == null ? true : _canexecute(parameter);
        }

        public void Execute(object parameter)
        {
            _action.Invoke(parameter);
        }
    }
}
