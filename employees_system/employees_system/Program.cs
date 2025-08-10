using employees_system.Mapper;
using employees_system.Models;
using employees_system.Repositories.EmployeePropertyRepo;
using employees_system.Repositories.EmployeeRepo;
using employees_system.Repositories.PropertyDefinitionRepo;
using employees_system.Repositories.PropertyOptionRepo;
using employees_system.Services.EmployeeService;
using employees_system.Services.PropertyService;
using employees_system.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace employees_system
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            builder.Services.AddScoped<IEmployeePropertyRepo, EmployeePropertyRepo>();
            builder.Services.AddScoped<IPropertyDefinitionRepo, PropertyDefinitionRepo>();
            builder.Services.AddScoped<IPropertyOptionRepo, PropertyOptionRepo>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IPropertyService, PropertyService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingConfig>());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
