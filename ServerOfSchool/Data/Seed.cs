using Microsoft.AspNetCore.Identity;
using ServerOfSchool.Models;
using System.Net;

namespace ServerOfSchool.Data
{
    public class Seed
    {

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.Teacher))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Teacher));
                if (!await roleManager.RoleExistsAsync(UserRoles.student))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.student));

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "Alhammami504@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        UserName = "Mansour",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        FirstName = "mansour",
                        LastName = "alhammami",
                        DateOfBirth = DateTime.Now
                    };
                    await userManager.CreateAsync(newAdminUser, "Mansour@504");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);

                }
            }
        }
    }
}
