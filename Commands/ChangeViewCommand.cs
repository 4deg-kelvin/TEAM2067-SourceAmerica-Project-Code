using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TEAM2067_SourceAmerica_Project.ViewModels;

namespace TEAM2067_SourceAmerica_Project.Commands
{
    public class ChangeViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private MainViewModel _viewModel;


        public ChangeViewCommand(MainViewModel viewModel)
        {
            this._viewModel = viewModel;
            
        }

        public bool CanExecute(object parameter)
        {
            //Change for later?
            return true;
        }

        public void Execute(object parameter)
        {
            string indicatedView = parameter.ToString();

            if (indicatedView == "Login")
            {
                _viewModel.CurrentView = new LoginViewModel();
            }
            else if (indicatedView == "EmployerHub")
            {
                _viewModel.CurrentView = new EmployerHubViewModel();
            }
            else if (indicatedView == "Manage_Employees")
            {
                _viewModel.CurrentView = new ManageEmployeesViewModel();
            }
           
        }
    }
}
