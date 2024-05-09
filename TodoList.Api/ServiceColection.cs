using System.Reflection;
using TodoList.Api.Services.Implementation;
using TodoList.Api.Services.Interfaces;
using TodoList.DAL;
//using TodoList.DAL.Core.Configuration;

namespace TodoList.Api
{
    public static class ServiceColection
    {
        public static IServiceCollection AddTodoListApiServices(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services
                .AddTodoDbContextServices()
                .AddCQRS();

            services.AddScoped<IUserTasksService, UserTaskService>();
            services.AddScoped<ISubTaskService, SubTaskService>();
            services.AddScoped<IUserService, UserService>();



            //services.AddIdentityServer()
            //    .AddInMemoryClients(new List<Client>())
            //    .AddInMemoryIdentityResources(new List<IdentityResource>())
            //    .AddInMemoryApiResources(new List<ApiResource>())
            //    .AddInMemoryApiScopes(new List<ApiScope>())
            //    .AddTestUsers(new List<TestUser>())
            //    .AddDeveloperSigningCredential();

            var provider = services.BuildServiceProvider();
            //var settings = provider.GetRequiredService<IOptions<JwtTokenSettings>>().Value;


            //services.AddAuthentication().AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        ValidateLifetime = true,
            //        ValidateAudience = false,
            //        ValidateIssuer = false,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Token))

            //    };
            //});



            return services;
        }
    }
}
