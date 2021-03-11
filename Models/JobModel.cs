using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace TEAM2067_SourceAmerica_Project.Models
{
    public class JobModel
    {
        public JobModel()
        {
            this.Salarys = new HashSet<Salary>();
            this.Employees = new HashSet<EmployeeModel>();
        }
        public string JobName { get; set; }
        [Key]
        public int JobId { get; set; }
        public string CurrentSessionType { get; set; }
        public Salary CurrentSessionSalary { get; set; }

        public virtual ICollection<Salary> Salarys { get; set; }
        public virtual ICollection<EmployeeModel> Employees { get; set; }

    }
}
