using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TodoList.DAL;
using TodoList.DAL.Entities;
using ToDoList.Server.Services;

namespace ToDoList.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            var assembly = typeof(Program).Assembly.GetName().Name;
            var defaultConnString = builder.Configuration.GetConnectionString("ToDoListBase");

            //REJESTRACJA//
            if (true)
            {
                SeedData.EnsureSeedData(defaultConnString);
            }

            //builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();

            builder.Services.AddDbContext<TodoDbContext>(options =>
                options.UseSqlServer(defaultConnString,
                 b => b.MigrationsAssembly(assembly)));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TodoDbContext>()
                .AddDefaultTokenProviders();
            //

            builder.Services.AddRazorPages();



            builder.Services.AddIdentityServer()
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                    b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                    b.UseSqlServer(defaultConnString, opt => opt.MigrationsAssembly(assembly));
                })
                .AddDeveloperSigningCredential()
                .AddProfileService<CustomProfileService>()
                .AddServerSideSessions();
            //.AddInMemoryIdentityResources(Config.IdentityResources); ;

            builder.Services.RemoveAll<IProfileService>();
            builder.Services.AddScoped<IProfileService, CustomProfileService>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseIdentityServer();

            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapRazorPages().RequireAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.Run();
        }
    }
}