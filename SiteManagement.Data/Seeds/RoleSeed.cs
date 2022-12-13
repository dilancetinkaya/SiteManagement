using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiteManagement.Infrastructure.Seeds
{
    /// <summary>
    /// admin bilgilerini ekleme.
    /// </summary>
    public static class RoleSeed
    {
        public static async Task SeedRolesAsync( RoleManager<Role> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new Role { Name = "Admin" });
        }

        public static async Task SeedSuperAdminAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            //Seed Default User
            var adminUser = new User
            {
                UserName = "siteadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Dilan",
                LastName = "Cetinkaya",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
           
                    await userManager.CreateAsync(adminUser, "AdminPassword123");
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    await userManager.AddToRoleAsync(adminUser, "User");

        }
    }
  }







