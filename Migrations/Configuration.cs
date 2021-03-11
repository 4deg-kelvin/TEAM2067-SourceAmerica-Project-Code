namespace TEAM2067_SourceAmerica_Project.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TEAM2067_SourceAmerica_Project.Models.EmployeesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "TEAM2067_SourceAmerica_Project.Models.EmployeesContext";
        }

        protected override void Seed(TEAM2067_SourceAmerica_Project.Models.EmployeesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
