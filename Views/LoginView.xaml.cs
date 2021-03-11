using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TEAM2067_SourceAmerica_Project.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();

            TimeLabel.Text = DateTime.Now.ToLongTimeString();   

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); //update time every one second
            timer.Tick += (s, args) => { TimeLabel.Text = DateTime.Now.ToLongTimeString(); };
            timer.Start();
        }
    }
}
