using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Projekt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var db = new AppDbContext())
            {
                db.Database.Migrate();

                DbSeeder.Seed(db);
                if (!db.Users.Any())
                {
                    db.Users.Add(new User()
                    {
                        Login = "admin",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin")
                    });
                    db.SaveChanges();
                }
            }
        }
    }

}
