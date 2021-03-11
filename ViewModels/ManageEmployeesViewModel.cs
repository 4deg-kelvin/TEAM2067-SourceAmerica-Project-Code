using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEAM2067_SourceAmerica_Project.Models;

namespace TEAM2067_SourceAmerica_Project.ViewModels
{
    public class ManageEmployeesViewModel : BaseViewModel
    {
        public EmployeeModel SelectedEmployee { get; set; } = null;
        public int EmployeeID { get; set; } 
    }
}
