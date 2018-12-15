using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVekilApplication.Models
{
    public class UserAndRoleCreater
    {
        public static async Task CreateAsync(IServiceScope scope, IdentityDbContext<User> db)
        {
            if (!db.Users.Any() && !db.Roles.Any())
            {

                UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                User user = new User()
                {
                    Name = "Murad",
                    Surname = "Ibrahimkhanli",
                    UserName = "ibrahimkhanli",
                    Email = "ibrahimxanlimurad@hotmail.com",
                    RegisterDate = DateTime.Now
                };

                User user2 = new User()
                {
                    Name = "Tarlan",
                    Surname = "Usubov",
                    UserName = "Usubsoy",
                    Email = "tarlan@e-vekil.az",
                    RegisterDate = DateTime.Now
                };

                User user3 = new User()
                {
                    Name = "Nihad",
                    Surname = "Aliyev",
                    UserName = "NihadAliyev",
                    Email = "nihad@e-vekil.az",
                    RegisterDate = DateTime.Now
                };
                var identityResult = await userManager.CreateAsync(user, "Murad1234@");
                var identityResult2 = await userManager.CreateAsync(user2, "Terlan123@");
                var identityResult3 = await userManager.CreateAsync(user3, "Nihad123@");
                if (identityResult.Succeeded)
                {
                    await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                    await roleManager.CreateAsync(new IdentityRole() { Name = "User" });
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                if (identityResult2.Succeeded)
                {
                    await userManager.AddToRoleAsync(user2, "Admin");
                }
                if (identityResult3.Succeeded)
                {
                    await userManager.AddToRoleAsync(user3, "Admin");
                }

                await db.SaveChangesAsync();
            }
        }
    }
}
