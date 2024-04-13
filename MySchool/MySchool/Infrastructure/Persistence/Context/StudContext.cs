using Microsoft.EntityFrameworkCore;
using MySchool.Core.Domain.Entities;

namespace MySchool.Infrastructure.Persistence.Context
{
    public class StudContext : DbContext
    {
        public StudContext(DbContextOptions<StudContext> opt) : base(opt) 
        {
        }


        public DbSet<Class> Classes => Set<Class>();
        public DbSet<Guardian> Guardians => Set<Guardian>();        
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(
                new Role{Id = "abcd",Name = "guardian" },
                new Role{Id = "lmno",Name = "student"},
                new Role{Id = "rxyz",Name = "teacher"},
                new Role{Id = "1234",Name = "superAdmin"});

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "asdf",
                    FirstName = "super",
                    LastName = "admin",
                    Email = "admin@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("pass"),
                });

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = "pole", UserId = "asdf", RoleId = "1234" });
        }
    }
}
