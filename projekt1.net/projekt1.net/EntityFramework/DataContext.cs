using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projekt1.net.Models;

namespace projekt1.net.EntityFramework
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobOffer> JobOfers { get; set; }
        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<JobOffer>().HasData(
                new JobOffer
                {
                    CompanyId = 1,
                    Id = 1,
                    Description = "You can teach math in high school",
                    JobTitle = "Teacher",
                    SalaryFrom = 1000,
                    SalaryTo = 10000,
                    Location = "Warsaw",
                    Created = new DateTime(2020, 01, 06),
                    ValidUntil = new DateTime(2022, 01, 20)

                },
                new JobOffer
                {
                    CompanyId = 2,
                    Id = 2,
                    Description = "You will be able to lead the project",
                    JobTitle = "Manager",
                    SalaryFrom = 10000,
                    SalaryTo = 20000,
                    Location = "Warsaw",
                    Created = new DateTime(2020, 01, 06),
                    ValidUntil = new DateTime(2022, 01, 20)

                }




                );
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> User { get; set; }



    }
}
