using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.EntityFramework.Storage;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoList.DAL;
using TodoList.DAL.Entities;

namespace ToDoList.Server
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();

            services.AddDbContext<TodoDbContext>(
                options => options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TodoDbContext>()
                .AddDefaultTokenProviders();

            services.AddOperationalDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
                });

            services.AddConfigurationDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
                });

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

            var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            context.Database.Migrate();

            EnsureSeedData(context);

            var ctx = scope.ServiceProvider.GetService<TodoDbContext>();
            ctx.Database.Migrate();
            EnsureUsers(scope);
        }


        private static void EnsureUsers(IServiceScope scope)
        {

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var rafal = userMgr.FindByNameAsync("Rafal").Result;

            if (rafal == null)
            {
                rafal = new ApplicationUser
                {
                    UserName = "Rafal",
                    Email = "rafal@email.com",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(rafal, "Abc123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result =
                    userMgr.AddClaimsAsync(
                        rafal,
                        new Claim[]
                        {
                        new Claim(JwtClaimTypes.Name, "Rafal C"),
                        new Claim(JwtClaimTypes.GivenName, "Rafal"),
                        new Claim(JwtClaimTypes.FamilyName, "C"),
                        new Claim(JwtClaimTypes.WebSite, "youtube.com"),
                        new Claim("location", "somewhere")
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }
        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients.ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources.ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes.ToList())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in Config.ApiResources.ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

        }
    }
}
