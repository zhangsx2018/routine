using Microsoft.EntityFrameworkCore;
using routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace routine.Api.Data
{
    public class RoutineDbContext :DbContext
    {
        public RoutineDbContext(DbContextOptions<RoutineDbContext> options) 
            :base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().Property(x => x.name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Company>().HasData(

                new Company
                {
                    id = Guid.Parse("bbdee09c-089b-4d30-bece-44df5923716c"),
                    name = "Microsoft",
                    Introduction = "Great Company"
                },
                new Company
                {
                    id = Guid.Parse("6fb600c1-9011-4fd7-9234-881379716440"),
                    name = "Google",
                    Introduction = "Don't be evil"
                },
                new Company
                {
                    id = Guid.Parse("5efc910b-2f45-43df-afae-620d40542853"),
                    name = "Alipapa",
                    Introduction = "Fubao Company"
                }
            );
        }
    }
}
