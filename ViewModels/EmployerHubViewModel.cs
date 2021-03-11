using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PropertyChanged;
using TEAM2067_SourceAmerica_Project.Commands;
using TEAM2067_SourceAmerica_Project.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace TEAM2067_SourceAmerica_Project.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class EmployerHubViewModel : BaseViewModel
    {
        private DataAccess _dataAccess = new DataAccess();
        public EmployerHubViewModel()
        {
            ExportToExcel = new RelayCommand(DisplayInExcel, (obj) => { return true; });
        }
        public RelayCommand ExportToExcel { get; set; }
        public ObservableCollection<EmployeeModel> Employees
        {
            get { 
                DataAccess ds = new DataAccess();
                return ds.GetAllEmployees();
            }
        }
        /// <summary>
        /// Displays an excel form with employee details
        /// </summary>
        /// <param name="parameter"></param>
        private void DisplayInExcel(object parameter)
        {
            //Launch new application
            var exelApp = new Excel.Application();
            var underlyingApplication = exelApp.Application;
            //Show excel application
            exelApp.Visible = true;
            //Add a new workbook
            exelApp.Workbooks.Add();

            // Asisgn workshet to the current sheet being used
            Excel.Worksheet currentWorksheet = (Excel.Worksheet)exelApp.ActiveSheet;
            // Use this range for "headers" (to be bold)
            Excel.Range titleRange = exelApp.Range[currentWorksheet.Cells[1, "A"], currentWorksheet.Cells[1, "L"]];

            // Set "header" fonts and colors
            titleRange.Font.Bold = true;
            titleRange.Font.Size = 13;
            titleRange.Borders.Color = Excel.XlRgbColor.rgbGreen;

            //Get employees
            var employees = _dataAccess.GetAllEmployees();
            EmployeeModel test = new EmployeeModel();
            
            //Set rows and colums for foreach loop
            int row = 1;
            int column = 1;

            var properties = test.GetType().GetProperties(); //Get properties of EmployeeModel
            //Output into the top row the property names
            for (int i = 1; i < properties.Length; i++)
            {
                currentWorksheet.Cells[1, i] = properties[i].Name.ToString();
            }
            foreach (var employee in employees)
            {
                row++;
                currentWorksheet.Cells[row, "A"] = employee.FirstName;
                currentWorksheet.Cells[row, "B"] = employee.LastName;
                currentWorksheet.Cells[row, "C"] = employee.DateOfBirth;
                currentWorksheet.Cells[row, "D"] = employee.RFID_ID;
                currentWorksheet.Cells[row, "E"] = employee.Fullname;
                currentWorksheet.Cells[row, "F"] = employee.JobId;
                currentWorksheet.Cells[row, "G"] = employee.CurrentSalary != null ? (double?)employee.CurrentSalary.PayPerHour : (double?)0;
                currentWorksheet.Cells[row, "H"] = employee.Job.JobName;
                currentWorksheet.Cells[row, "I"] = employee.ClockedInTime.ToString();
                currentWorksheet.Cells[row, "J"] = employee.HoursWorked;
                currentWorksheet.Cells[row, "K"] = employee.TotalPay.ToString("C",CultureInfo.CurrentCulture);
                currentWorksheet.Cells[row, "L"] = "n/a";
            }
            // Auto fit all columns
            for (int i = 1; i < currentWorksheet.UsedRange.Columns.Count; i++ )
            {
                currentWorksheet.Columns[i].AutoFit();
            }
            currentWorksheet.Rows[1].AutoFit();
            currentWorksheet.Rows[2].AutoFit();
        }
        
    }
}
