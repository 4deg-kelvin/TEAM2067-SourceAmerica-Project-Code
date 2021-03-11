using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace TEAM2067_SourceAmerica_Project.Models
{
    /// <summary>
    /// Business layer class, which gets data from data access and manipulates them as needed
    /// </summary>
    public class BusinessLayer
    {
        private DataAccess BusinessLayerDataAccces; //field to provide access to DataAccess

        public BusinessLayer()
        {
            this.BusinessLayerDataAccces = new DataAccess();
        }
        /// <summary>
        /// Determines what to do with a logging in employee, and determines their current salary rates. Returns the employee as indicated by rfid_ID
        /// </summary>
        /// <param name="rfid_ID"></param>
        public void ChooseSessionStart(EmployeeModel employee)
        {
            //Query required info regarding salaries
            
            var EmployeeSalaries = BusinessLayerDataAccces.GetAllEmployeeSalaries(employee.EmployeeId);
            var lunchSalary = EmployeeSalaries.Where(x => x.SalaryName.Contains("lunch")).First();
            var normalSalary = EmployeeSalaries.Where(x => x.SalaryName.Contains("normalPay")).First();
            var currentTime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));

            using (var ctx = new EmployeesContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                var EmployeeJob = ctx.JobModels.Find(employee.JobId); //workaround to null error
                
                if (currentTime >= lunchSalary.StartTime && currentTime <= lunchSalary.EndTime) //is it lunch time? set lunch salary if such
                {
                    if (employee.ClockedInTime != null) // if employee is currently working...
                    {
                        bool sessionEnded = EndSession(employee.EmployeeId); // end session 
                        if (sessionEnded)
                        {
                            StartSession(employee.EmployeeId, lunchSalary.SalaryId);
                        }
                        else
                        {
                            throw new Exception("Session couldn't end");

                        }
                    }

                }
                else if ((currentTime >= normalSalary.StartTime && currentTime <= normalSalary.EndTime)) // if employee is in working hours
                {
                    if (employee.ClockedInTime == null)
                        StartSession(employee.EmployeeId, normalSalary.SalaryId);
                    else
                        EndSession(employee.EmployeeId);
                }
                else
                {
                    MessageBox.Show("You are not within working hours");

                }
                     
            }
        }
        /// <summary>
        /// Starts a new session for employee, returns bool val for success state
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public bool StartSession(int EmployeeID, int SalaryId)
        {
            using (var ctx = new EmployeesContext())        
        
            {
                //Query here to be in the same context
                EmployeeModel Employee = ctx.Employees.Find(EmployeeID);
                var Job = ctx.JobModels.Find(Employee.JobId);
                var query = ctx.Salaries.Where(salary => salary.Jobs.Any(job => job.JobName == Job.JobName)).ToList();
                var collection = new ObservableCollection<Salary>(query);

                if (Employee != null)
                {
                    //Sets clocked in time to now
                     Employee.ClockedInTime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
                    //Find employee's salary object
                    var indicatedSalary = collection.Where(x => x.SalaryId == SalaryId).First();
                    //Set current salary object to the one found by indicatedSalary
                    Employee.CurrentSalary = indicatedSalary;


                    ctx.SaveChanges();
                    return true;
                }
                else
                    return false; 
            }
        }
        /// <summary>
        /// Ends a current session, by replacing the clock in time, calculating the hours worked, and inputting that into Employee's totalhours
        /// </summary>
        /// <remarks>
        /// NOTE THAT HOURS ARE NOT ROUNDED AND ANY VALUES NOT IN HOUR'S PLACE ARE TRUNCATED
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool EndSession(int id)
        {
            using (var ctx = new EmployeesContext())
            {
                EmployeeModel Employee = ctx.Employees
                    .Include(x => x.CurrentSalary)
                    .Where(x => x.EmployeeId == id).First();
    
                    
                var currentTime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
                if (Employee != null && Employee.ClockedInTime != null)
                {
                    if (Employee.ClockedInTime <= currentTime)
                    {
                        //Calculate time worked in hours
                        TimeSpan TimeWorked = ((TimeSpan)(currentTime - Employee.ClockedInTime));

                        //Add hours worked to employee's record
                        Employee.HoursWorked += TimeWorked.Hours;

                        //Reset employee's clocked in time, note that ClockedInTime is null, as datetime will be assigned by StartSession()
                        Employee.ClockedInTime = null;

                        //Calculate pay for session
                        double TotalPay = (TimeWorked.Hours * Employee.CurrentSalary.PayPerHour);

                        //Assign pay for session to Employee Records
                        Employee.TotalPay += TotalPay;

                        ctx.SaveChanges();
                        return true;
                    }
                    else return false;
                }
                else 
                    return false;
            }
        }
        


    }
}
