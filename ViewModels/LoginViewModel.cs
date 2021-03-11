using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using PropertyChanged;
using System.Timers;
using TEAM2067_SourceAmerica_Project.Commands;
using TEAM2067_SourceAmerica_Project.Models;
using System.Windows;

namespace TEAM2067_SourceAmerica_Project.ViewModels
{
    /// <summary>
    /// Login user, note that the login data will probably be from serial port
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class LoginViewModel : BaseViewModel
    {
        private BusinessLayer _businessLayer = new BusinessLayer();
        private DataAccess _dataAccess = new DataAccess();
        public bool LoginSucessful { get; set; }
        public string LoginFirstName { get; set; }
        public EmployeeModel LoggingInEmployee { get; set; }
        public RelayCommand LoginCommand { get; set; }
        public bool SerialPortConnected { get; set; } = false;
        public RelayCommand TryConnectSerialPortCommand { get; set; }
        public bool ChooseSessionStart { get; set; } = false;

        public LoginViewModel()
        {
            SerialPortAccess.NewIdPresent += SerialPortAccess_NewIdPresent; //subscribe new id to handler for rfid input
            TryConnectSerialPortCommand = new RelayCommand(TryConnectSerialPort, (obj) => { return true; });
        }       

        private void SerialPortAccess_NewIdPresent(object sender, string rfid_id)
        {
            
            if (rfid_id == null) 
                //Shouldn't be null, as SerialPort detects for this
                throw new ArgumentNullException(); 
            else
            {
                //Assign employee frorm rfid to current logging in employee
                EmployeeModel employee = _dataAccess.SearchByEmployeeRFID(rfid_id);
                //Assign logging in employee to one found, otherwise assign null (kinda useless)
                LoggingInEmployee = employee ?? null;
                //Start Session
                if(ChooseSessionStart)
                    _businessLayer.ChooseSessionStart(employee);
                
                // Start timer to automatically reset Logging in employee to null after a while
                var timer = new Timer();
                var currentEmployee = LoggingInEmployee;
                timer.Elapsed += (s, args) =>
                {
                    if (currentEmployee == LoggingInEmployee)
                        LoggingInEmployee = null;
                    timer.Stop(); //Stop timer
                    timer.Dispose();
                };
                timer.Interval = 4000; // 4 seconds until reset
                timer.Start();
                


            }
        }
        private void TryConnectSerialPort(object parameter)
        {
            try
            {
                if (!SerialPortAccess.SerialIsOpen)
                {
                    SerialPortAccess.StartCOMPort();
                    SerialPortConnected = true;
                }
                else
                    MessageBox.Show("Serial Port is already open");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not connect COM port");
            }
        }
        
    }
}
