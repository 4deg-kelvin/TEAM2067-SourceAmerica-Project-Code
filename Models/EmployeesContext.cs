using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEAM2067_SourceAmerica_Project.Models
{
    public class EmployeesContext : DbContext
    {
        public EmployeesContext() : base(@"ADO.Net Model")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EmployeesContext, TEAM2067_SourceAmerica_Project.Migrations.Configuration>());
            this.Employees.Include(x => x.Job);
            this.JobModels.Include(x => x.Employees);
            this.Employees.Include(x => x.CurrentSalary);
            this.Salaries.Include(x => x.EmployeeModel);
            this.Configuration.LazyLoadingEnabled = false;
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobModel>()
                .HasMany<Salary>(s => s.Salarys)
                .WithMany(c => c.Jobs)
                .Map(cs =>
                {
                    cs.MapLeftKey("JobID");
                    cs.MapRightKey("SalaryID");
                    cs.ToTable("JobsSalarys");
                });
            modelBuilder.Entity<JobModel>()

                .HasMany<EmployeeModel>(s => s.Employees)
                .WithRequired(e => e.Job)
                .HasForeignKey<int>(e => e.JobId);
            
                
                
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<JobModel> JobModels { get; set; }
        public DbSet<Salary> Salaries { get; set; }

    }
}
