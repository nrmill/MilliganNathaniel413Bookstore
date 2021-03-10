using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MilliganNathaniel413Bookstore.Models;

namespace MilliganNathaniel413Bookstore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // this is a new service we added to connect to DB
            services.AddDbContext<BookstoreDbContext>(options =>
               {
                   options.UseSqlite(Configuration["ConnectionStrings:BookstoreConnection"]);
               });

            //another new service to implement scope
            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();

            //allows use of Razor Pages
            services.AddRazorPages();

            //enables local session storage
            services.AddDistributedMemoryCache();
            services.AddSession();

            //session cart allowed to perform functions and be read
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    "catpage",
                    "{category}/p{pageNum:int}",
                    new { Controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    "pagination",
                    "p{pageNum:int}",
                    new { Controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    "page",
                    "{category}",
                    new { Controller = "Home", action = "Index", page = 1 });
                

                endpoints.MapDefaultControllerRoute();

                //allows use of Razor Pages
                endpoints.MapRazorPages();
            });

            //commented out after first run of app (that first run with this line sets up the seed data in the database)
            //used again after updating seed data with NumPages and adding new books
            //used again after updating seed data with appropriate Classification and Category
            //used again after refactoring to use Sqlite db
            //SeedData.EnsurePopulated(app);
        }
    }
}
