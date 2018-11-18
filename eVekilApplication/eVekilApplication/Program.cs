using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eVekilApplication.Data;
using eVekilApplication.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace eVekilApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost webHost = CreateWebHostBuilder(args).Build();

            using (IServiceScope scopedService = webHost.Services.CreateScope())
            {
                using (EvekilDb dbContext = scopedService.ServiceProvider.GetRequiredService<EvekilDb>())
                {
                    //if (!dbContext.Users.Any())
                    //{
                    //    //#region Admins


                    //    //#endregion

                    //    //dbContext.Games.AddRange();
                    //    //dbContext.SaveChanges();
                    //}

                    UserAndRoleCreater.CreateAsync(scopedService, dbContext).Wait();

                }
            }

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
