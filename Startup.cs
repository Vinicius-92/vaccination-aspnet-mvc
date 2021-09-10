using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Vaccination.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vaccination.Services;
using System.Threading.Tasks;
using System;

namespace Vaccination
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(  
                    Configuration.GetConnectionString("DefaultConnection")));
            
            
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                        .AddRoles<IdentityRole>()
                        .AddDefaultUI()
                        .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

           services.AddScoped<AddressService>();
           services.AddScoped<VaccinationPointService>();
           services.AddScoped<PersonService>();
           services.AddScoped<VaccineService>();
           services.AddScoped<VaccineBatchService>();
           services.AddScoped<VaccinationRecordService>();
           services.AddScoped<SeedingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                seedingService.Seed();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            CreateRoles(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            // var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] rolesNames = { "admin", "User", "Operator" };
            IdentityResult result;
            foreach (var namesRole in rolesNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(namesRole);
                if (!roleExist)
                {
                    result = await roleManager.CreateAsync(new IdentityRole(namesRole));
                }
            }
        }
    }
}
