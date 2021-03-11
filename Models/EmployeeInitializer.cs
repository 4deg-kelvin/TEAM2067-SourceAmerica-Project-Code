using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAM2067_SourceAmerica_Project.Models
{
    public class EmployeeInitializer : DropCreateDatabaseIfModelChanges<EmployeesContext>
    {
        protected override void Seed(EmployeesContext context)
        {
            IList<EmployeeModel> defaultEmployee = new List<EmployeeModel>();

            defaultEmployee.Add(new EmployeeModel() { FirstName = "Joe", LastName = "Kanley" });

            context.Employees.AddRange(defaultEmployee);
            base.Seed(context);
        }
    }
}
