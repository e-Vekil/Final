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
                var identityResult = await userManager.CreateAsync(user, "Murad1234@");
                if (identityResult.Succeeded)
                {
                    await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                    await roleManager.CreateAsync(new IdentityRole() { Name = "User" });
                    await userManager.AddToRoleAsync(user, "Admin");
                }


                await db.SaveChangesAsync();
            }
        }
    }
}
