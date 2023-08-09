using aryamoneygrow;
using aryamoneygrow.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stockmantras.Models;
using Stockmantras.Repository;
using System;

namespace Stockmantras
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



            services.AddCors();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddHttpClient();
            //services.AddDbContext<AppDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //added the below mentioned code to avoid 
            services.AddDbContext<AppDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
             maxRetryCount: 5,
             maxRetryDelay: TimeSpan.FromSeconds(30),
             errorNumbersToAdd: null)));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<AppDBContext>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

            });
            //adding role



            //adding authorization for admin
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserRolePolicy", policy =>
                    policy.RequireRole("Admin","Coin","User"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)
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
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            RoleInitializer.InitializeAsync(roleManager).Wait();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            // Inside the Configure method in Startup.cs
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "news",
                    pattern: "News/DetailedNews/{id}",
                    defaults: new { controller = "News", action = "DetailedNews" });
                
               /* endpoints.MapControllerRoute(
                    name: "equitySuggestions",
                    pattern: "User/GetEquitySuggestions",
                    defaults: new { controller = "User", action = "GetEquitySuggestions" });*/

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });



        }
    }
}
