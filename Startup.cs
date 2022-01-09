using System;
using FileExchanger.Domain.DB;
using FileExchanger.Domain.Models.People;
using FileExchanger.Infrastructure;
using FileExchanger.Infrastructure.Guarantors;
using FileExchanger.Service;
using FileExchanger.Service.Account;
using FileExchanger.Service.BasicData;
using FileExchanger.Service.Publications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileExchanger
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

            services.AddTransient<IPublicable, PublicationsService>();
            services.AddTransient<IDictionaire, DictinaryService>(); 
            services.AddTransient<IAccounting, AccountService>();
            services.AddDbContext<FileExchangerDbContext>(options =>
            {
                options.UseNpgsql("Username=postgres; Database=FileExchanger; Password=111111; Host=localhost");
            });

            services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<FileExchangerDbContext>();

            var serviceProvider = services.BuildServiceProvider();
            var guarantor = new SeedDataGuarantor(serviceProvider);
            guarantor.EnsureAsync();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var guarantors = scope.ServiceProvider.GetServices<IStartupPreConditionGuarantor>();
                try
                {
                    Console.WriteLine("Startup guarantors started");
                    foreach (var guarantor in guarantors)
                        guarantor.Ensure(scope.ServiceProvider);

                    Console.WriteLine("Startup guarantors executed successfuly");
                }
                catch (StartupPreConditionException)
                {
                    Console.WriteLine("Startup guarantors failed");
                    throw;
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
