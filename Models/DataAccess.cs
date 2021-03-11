using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PropertyChanged;
using System.Data.Entity;


namespace TEAM2067_SourceAmerica_Project.Models
{
    [AddINotifyPropertyChangedInterface]
    public class DataAccess
    {

        public ObservableCollection<EmployeeModel> GetAllEmployees()
        {
            using (var ctx = new EmployeesContext())
            {
                var query = (from s in ctx.Employees
                             .Include(x => x.Job)
                             .Include(x => x.CurrentSalary.EmployeeModel)
                             .Include(x => x.CurrentSalary)
                             select s).ToList();
                var ObservableEmployeeCollection = new ObservableCollection<EmployeeModel>(query);
                return ObservableEmployeeCollection;
            }
        }
        /// <summary>
        /// Returns all employee names, queries all employees, NEEDS TO BE PURPOSE FIXED
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<EmployeeModel> GetAllEmployeeNames()
        {
            using (var ctx = new EmployeesContext())
            {

                var query = (from s in ctx.Employees
                             select s).ToList();
                var ObservableCollectionEmployeeName = new ObservableCollection<EmployeeModel>(query);
                return ObservableCollectionEmployeeName;

            }
        }
        /// <summary>
        /// Searchs for an employee based on first name. Note that there are overloads for different search parameters
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public EmployeeModel SearchForEmployeeName(string name)
        {
            using (var ctx = new EmployeesContext())
            {
                EmployeeModel query;
                try
                {
                    query = (from s in ctx.Employees
                             where s.FirstName == name
                             select s).FirstOrDefault();
                }
                catch (Exception error)
                {

                    ShowError(error);
                    return null;
                }
                if (query != null)
                    return query;
                else
                    return null;

            }
        }
        public EmployeeModel SearchForEmployeeID(int id)
        {
            using (var ctx = new EmployeesContext())
            {
                EmployeeModel query;
                try
                {
                        query = (from s in ctx.Employees
                                 where s.EmployeeId == id
                                 select s).First();
                }
                catch (Exception error)
                {
                    ShowError(error);
                    query = null;
                    
                }
                if (query != null)
                    return query;
                else
                    return null;
            }
        }
        public EmployeeModel SearchByEmployeeRFID(string rfid_id)
        {
            using (var ctx = new EmployeesContext())
            {
                //Cleans rfid_id from carriage returns or newlines 
                char[] charsToTrim = { '\r', '\n' };
                string cleanID = rfid_id.Trim(charsToTrim);

                var query = (from s in ctx.Employees
                             where s.RFID_ID == cleanID
                             select s).First();
                if (query != null)
                    return query;
                else
                    return null;
                
            }
        }
        /// <summary>
        /// Return the indicated salary of an employee. Does not return a list. 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="desiredSalary"></param>
        /// <returns></returns>
        public Salary GetSalary (int employeeID, string desiredSalary)
        {
            using (var ctx = new EmployeesContext())
            {
                var employee = ctx.Employees.Find(employeeID);
                var Job = employee.Job;
                //var SalaryObject = ctx.Salaries 
                

                Salary query;

                try
                {
                    var SalaryObject = ctx.Salaries.Where(x => x.SalaryName.Contains(desiredSalary)).FirstOrDefault();
                    query = Job.Salarys.Where(salary => salary == SalaryObject).First();
                    //var query = ctx.JobModels.Where(job => job.Salarys.Any(salary => salary.SalaryId == SalaryObject.SalaryId));
                }
                catch (Exception error)
                {
                    query = null;
                    ShowError(error);
                }
                return query;
            }    
        }
        public ObservableCollection<Salary> GetAllEmployeeSalaries (int employeeID)
        {
            using (var ctx = new EmployeesContext())
            {
                var employee = ctx.Employees.Find(employeeID);
                JobModel Job = ctx.JobModels.Find(employee.JobId);
                
                try
                {
                    var query = ctx.Salaries.Where(salary => salary.Jobs.Any(job => job.JobName == Job.JobName)).ToList();
                    return new ObservableCollection<Salary>(query);
                }
                catch (Exception error)
                {
                    
                    ShowError(error);
                    return null;
                }
                
            }
        }
        
        private Action<object> ShowError = (object error) => { MessageBox.Show("Cannot find resources" + error); };

        
    }
}
