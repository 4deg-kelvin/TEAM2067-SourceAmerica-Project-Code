using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TEAM2067_SourceAmerica_Project.Models;

namespace TEAM2067_SourceAmerica_Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                SerialPortAccess.StartCOMPort();
            }
            catch (Exception error)
            {

                MessageBox.Show("WARNING! Serial Connection could not be established." + '\n' + error);
            }
            
            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            SerialPortAccess.CloseCOMPort();
            base.OnExit(e);
        }
    }
}
