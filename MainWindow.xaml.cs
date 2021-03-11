using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using TEAM2067_SourceAmerica_Project.Models;
using TEAM2067_SourceAmerica_Project.ViewModels;
using System.Data.Entity.Infrastructure; // namespace for the EdmxWriter class
using System.Xml;
using System.IO;

namespace TEAM2067_SourceAmerica_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel()
            {
                CurrentView = new LoginViewModel()
            };


            //using (var ctx = new EmployeesContext())
            //{
            //    using (XmlWriter writer1 = XmlWriter.Create(@"D:\Kelvin's Files\test6"))
            //    {
            //        EdmxWriter.WriteEdmx(ctx, writer1);
            //    }
            //}
            //using (var ctx = new EmployeesContext())
            //{
            //    var job = ctx.JobModels.Where(x => x.JobName == "job2").FirstOrDefault();
            //    var salary = ctx.Salaries.Where(x => x.SalaryName.Contains("lunch")).First();

            //    job.Salarys.Add(salary);

            //    ctx.SaveChanges();
            //}




        }
    }
}
