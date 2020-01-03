using Microsoft.EntityFrameworkCore;
using routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace routine.Api.Data
{
    public class DeviceDbContext : DbContext
    {
        public DeviceDbContext(DbContextOptions<DeviceDbContext> options)
          : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<DeviceMain>()
            //        .HasOne(l => l.DeviceType)
            //        .WithOne( l =>l.DeviceMain)
            //        .HasForeignKey<DeviceMain>(l => l.deviceTypeId);

            //modelBuilder.Entity<DeviceFile>()
            //    .HasOne(file => file.DeviceMain)
            //    .WithMany(device => device.DeviceFiles);
          
            


        }

      
        public DbSet<DeviceMain> deviceMains { get; set; }
        public DbSet<DeviceType> deviceTypes { get; set; }
        public DbSet<DeviceStatus> deviceStatuses { get; set; }
        public DbSet<DeviceFile> deviceFiles { get; set; }
    }
}
