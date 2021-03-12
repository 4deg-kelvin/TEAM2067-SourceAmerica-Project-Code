using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEAM2067_SourceAmerica_Project.Models
{
    public class EmployeeModel
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } = new DateTime();
        public string RFID_ID { get; set; }
        
        public string Fullname
        {
            get { return $"{FirstName} {LastName}"; }
        }
        public int JobId { get; set; }
        public bool IsClockedIn { get; set; } = false;
        
        
        
        public  Salary CurrentSalary { get; set; }
        public virtual JobModel Job { get; set; }
        public TimeSpan? ClockedInTime { get; set; } = TimeSpan.Zero;
        

        public int HoursWorked { get; set; }
        public double TotalPay { get; set; } = 0;

        public ICollection<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();

        public override string ToString()
        {
            return Fullname;
        }
    }
}
