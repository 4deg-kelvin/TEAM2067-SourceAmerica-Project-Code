using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PropertyChanged;

namespace TEAM2067_SourceAmerica_Project.Models
{
    /// <summary>
    /// Provides access to the serial port. Most configurations are hardcoded
    /// </summary>
    public static class SerialPortAccess
    {
        static SerialPort _serialPort = new SerialPort();
        static public event EventHandler<string> NewIdPresent;

        public static bool SerialIsOpen
        {
            get => _serialPort.IsOpen;
            private set => SerialIsOpen = value;
        }
        public static string EmployeeSerialID { get; set; }

        /// <summary>
        /// Starts a COM Port for serial data to be inputted
        /// </summary>
        public static void StartCOMPort()
        {
            
            //Sanity check to make sure port was previously closed
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
                throw new Exception("Port was previously open, closing to ensure integrity of port");
            }

            //Set COM Port configurations
            //All configurations are HARD-CODED, may want to improve?
            _serialPort.BaudRate = 9600;
            _serialPort.PortName = "COM3"; //Only port available on this device
            _serialPort.NewLine = "|m"; //Read up to this character as a "line"
            _serialPort.DataReceived += OnDataRecieved; //Subscribe datarecieved to event handler
            _serialPort.ReadBufferSize = 8;
            _serialPort.ReadTimeout = 4000; //Give 4 seconds to read

            _serialPort.Open(); //Open port

        }
        private static void OnDataRecieved(object sender, SerialDataReceivedEventArgs e)
        {
            var serialPort = sender as SerialPort; //Gets the details of the serial port
            string inputString = null;             // Set to null to catch errors

            try
            {
                if (serialPort.BytesToRead > 0)
                    inputString = serialPort.ReadLine(); //Read from the serial port

            }
            catch (Exception error)
            {
                MessageBox.Show("Timeout Error"  + error);
            }

            finally
            {
                if (inputString != null)
                {
                    EmployeeSerialID = inputString;
                    OnNewIdPresent(); //Raise event to tell that an id is ready to be read
                                      //put business layer stuff here
                    _serialPort.DiscardInBuffer(); //flush in buffer after id is received (prevents infinite read and \n or \r in buffer)
                } 
                
            }


        }
        public static void CloseCOMPort()
        {
            //If not currently open, do nothing
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }
        private static void OnNewIdPresent()
        {
            EventHandler<string> handler = NewIdPresent;
            handler(null, EmployeeSerialID);
            
        }
        
    }
}
