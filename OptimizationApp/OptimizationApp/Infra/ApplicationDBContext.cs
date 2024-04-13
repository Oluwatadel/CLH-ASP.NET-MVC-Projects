using Microsoft.EntityFrameworkCore;
using OptimizationApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationApp.Infra
{
    public class ApplicationDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conStr = @"Server=localhost;port=3306;Uid=test;password=P@$$w0rd;database=testdb";
            optionsBuilder.UseMySql(conStr, ServerVersion.AutoDetect(conStr));
        }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().ToTable("student");
            modelBuilder.Entity<Student>().Property<long>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Student>().HasIndex(p => p.LastName);
        }
    }
}
