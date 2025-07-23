using Microsoft.EntityFrameworkCore;
using TaskAndTeamManagementSystem.Models;
using TaskAndTeamManagementSystem.Data;
using Microsoft.AspNetCore.Identity;

namespace TaskAndTeamManagementSystem.Data
{
    public static class DbInitializer
    {
        public static void Seed(Context context)
        {
            
            if (!context.Users.Any())
            {
                var hasher = new PasswordHasher<User>();

                // Create users with hashed passwords
                var admin = new User
                {
                    FullName = "Admin",
                    Email = "admin@demo.com",
                    Role = UserRole.Admin
                };
                admin.Password = hasher.HashPassword(admin, "Admin123!");

                var manager = new User
                {
                    FullName = "Manager",
                    Email = "manager@demo.com",
                    Role = UserRole.Manager
                };
                manager.Password = hasher.HashPassword(manager, "Manager123!");

                var employee = new User
                {
                    FullName = "Employee",
                    Email = "employee@demo.com",
                    Role = UserRole.Employee
                };
                employee.Password = hasher.HashPassword(employee, "Employee123!");

                context.Users.AddRange(admin, manager, employee);
                context.SaveChanges();
            }
        }
    }
}
