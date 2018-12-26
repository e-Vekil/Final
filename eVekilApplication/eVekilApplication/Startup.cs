using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVekilApplication.Data;
using eVekilApplication.Infrastructure.Email;
using eVekilApplication.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eVekilApplication
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EvekilDb>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
            });

            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(15);//You can set Time  
                options.Cookie.MaxAge = TimeSpan.FromMinutes(15);
            });

            services.AddAuthentication(options=> {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddFacebook(facebookOptions=> {
                    facebookOptions.AppId = "359770388182038";
                    facebookOptions.AppSecret = "e0d86dae757f12cccfacd46a0adae9c1";
                })
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = "64365837343-02dl1ff838pkuvvm66fpjmnvmvfo15t6.apps.googleusercontent.com";
                    googleOptions.ClientSecret = "8P_jEWGF7pYS1NYg-xBbc_E4";
                })
                   .AddCookie();
            services.Configure<EmailServiceOption>((option) => {

                option.DisplayName = "E-VAKIL.AZ";
                option.Email = "tarlanru@code.edu.az";
                option.Password = "terlan5253";
                option.EnableSSL = true;
                option.Host = "smtp.gmail.com";
                option.Port = 587;
            });


            services.AddSingleton<EmailService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<EvekilDb>()
                .AddDefaultTokenProviders();



            services.Configure<IdentityOptions>(options =>
            {

                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;

                options.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
                options.User.RequireUniqueEmail = true;

            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);

                options.LoginPath = "/Account/Registration";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
                app.UseDeveloperExceptionPage();

            app.UseSession();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes => {

                routes.MapRoute(
                    name: "Admin",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
