using IdentityProject.IdentityValidator;
using IdentityProject.Models;
using IdentityProject.Models.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(_ => _.UseSqlServer(Configuration["ConnectionStrings:SqlServerConnectionString"]));
            services.AddIdentity<AppUser, AppRole>(_ =>
            {
                _.Password.RequiredLength = 5;
                _.Password.RequireNonAlphanumeric = false;
                _.Password.RequireLowercase = false;
                _.Password.RequireUppercase = false;
                _.Password.RequireDigit = false;

                _.User.RequireUniqueEmail = true;
                _.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+"; // We add Turkish characters in here.
            }).AddUserValidator<CustomUserValidation>().AddPasswordValidator<CustomPasswordValidation>().AddEntityFrameworkStores<AppDbContext>();

            //We configured to identity up to here.

            services.ConfigureApplicationCookie(_ =>
            {
                _.LoginPath = new PathString("/Auth/Login");
                _.Cookie = new CookieBuilder
                {
                    Name = "AspNetCoreIdentityProjectCookie", //We set name to the cookie that will create.
                    HttpOnly = false, //We blocked in here that malicious people can access to our cookie from client-side.
                    Expiration = TimeSpan.FromMinutes(2), // We determine expiration value to cookie that will create.
                    SameSite=SameSiteMode.Lax, // We determine send to requests that don't be a reason top-level navigation.
                    SecurePolicy=CookieSecurePolicy.Always // We make accessible from HTTPS.
                };
                _.SlidingExpiration = true; //If a request is made within half of the Expiration period, it will reset the remaining half again to refresh the originally set duration.
                _.ExpireTimeSpan = TimeSpan.FromMinutes(2); // We set again expiration period in here because maybe default value destroy our value.
            });

            //We configured to cookie settings up to here.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();


            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auth}/{action=SignIn}");
            });
        }
        //pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}
