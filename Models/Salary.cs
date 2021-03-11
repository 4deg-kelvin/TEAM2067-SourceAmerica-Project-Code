using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEAM2067_SourceAmerica_Project.Models
{
    public class Salary
    {
        public Salary()
        {
            this.Jobs = new HashSet<JobModel>();
            this.EmployeeModel = new HashSet<EmployeeModel>();
        }
        [Key]
        public int SalaryId { get; set; }
        public string SalaryName { get; set; }
        public double PayPerHour { get; set; }
        
        public TimeSpan? StartTime { get; set; }
        
        public TimeSpan? EndTime { get; set; }
        
        public virtual ICollection<EmployeeModel> EmployeeModel { get; set; }
        public virtual ICollection<JobModel> Jobs { get; set; }
        
        

    }
}
