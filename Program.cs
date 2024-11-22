using ContractPoe.Models;
using Microsoft.EntityFrameworkCore;


namespace ContractPoe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // Register the DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ContractClaimPoe")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=LecturerClaims}/{action=Index}/{id?}");  // Set the default controller to LecturerClaims and action to Index

            app.Run();
        }
    }
}
