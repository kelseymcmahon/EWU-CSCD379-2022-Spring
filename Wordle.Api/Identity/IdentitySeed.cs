using Microsoft.AspNetCore.Identity;
using Wordle.Api.Data;

namespace Wordle.Api.Identity
{
    public static class IdentitySeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Roles
            await SeedRolesAsync(roleManager);

            // Seed Admin User
            await SeedAdminUserAsync(userManager);

            await SeedUsersAsync(userManager);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            // Seed Roles
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
            if (!await roleManager.RoleExistsAsync(Roles.Grant))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Grant));
            }
            if (!await roleManager.RoleExistsAsync(Roles.MasterOfTheUniverse))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.MasterOfTheUniverse));
            }
        }

        private static async Task SeedAdminUserAsync(UserManager<AppUser> userManager)
        {
            // Seed Admin User
            if (await userManager.FindByNameAsync("Admin@intellitect.com") == null)
            {
                AppUser user = new AppUser
                {
                    UserName = "Admin@intellitect.com",
                    Email = "Admin@intellitect.com",
                };

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd123").Result;

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.Admin);
                    await userManager.AddToRoleAsync(user, Roles.Grant);
                }
            }
        }

        private static async Task SeedUsersAsync(UserManager<AppUser> userManager) //Have to figure out how to set Dob for these to be meaningful
        {
            // Seed Admin User
            if (await userManager.FindByNameAsync("Motu@intellitect.com") == null)
            {
                AppUser user = new AppUser
                {
                    UserName = "Motu@intellitect.com",
                    Email = "Motu@intellitect.com",
                    Dob = DateTime.Today.AddYears(-50)
                };

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd123").Result;

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.MasterOfTheUniverse);
                }
            }

            if (await userManager.FindByNameAsync("MotuUnderage@intellitect.com") == null) 
            {
                AppUser user = new AppUser
                {
                    UserName = "MotuUnderage@intellitect.com",
                    Email = "MotuUnderage@intellitect.com",
                    Dob = DateTime.Today.AddYears(-5)
                };

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd123").Result;

            }

            if (await userManager.FindByNameAsync("Ms20@intellitect.com") == null) 
            {
                AppUser user = new AppUser
                {
                    UserName = "Ms20@intellitect.com",
                    Email = "Ms20@intellitect.com",
                    Dob = DateTime.Today.AddYears(-20)
                };

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd123").Result;
            }

            if (await userManager.FindByNameAsync("Mr26@intellitect.com") == null)
            {
                AppUser user = new AppUser
                {
                    UserName = "Mr26@intellitect.com",
                    Email = "Mr26@intellitect.com",
                    Dob = DateTime.Today.AddYears(-26)
                };

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd123").Result;
            }
        }
    }
}
