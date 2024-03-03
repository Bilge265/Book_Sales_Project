using EntityLayer.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class DataSeeder
    {
        public async Task Initialize(IServiceProvider serviceProvider,
                    UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var roleResult = await roleManager.CreateAsync(new AppRole { Name = roleName });
                }
            }

            bool adminExists = (await userManager.GetUsersInRoleAsync("Admin")).Count > 0;
            if (!adminExists)
            {
                AppUser user = await userManager.FindByEmailAsync("admin@gmail.com");
                if (user == null)
                {
                    user = new AppUser()
                    {
                        UserName = "Admin",
                        Email = "admin@gmail.com",
                        Name = "Admin",
                        Surname="admin"
                    };
                    await userManager.CreateAsync(user, "Admin123.");
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

        }
    }
}
