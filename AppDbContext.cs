using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Projekt.Models;

namespace Projekt
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Exit> Exits { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SchoolClass> Classes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=school.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword("admin");
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Login = "admin", PasswordHash = hash, UserType = UserType.Admin}
                );

            string hash2 = BCrypt.Net.BCrypt.HashPassword("teacher");
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 2, Login = "teacher", PasswordHash = hash2, UserType = UserType.Teacher }
                );
        }
    }
}
