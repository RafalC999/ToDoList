using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.DAL.Interceptors;


namespace TodoList.DAL
{
    public static class ServiceColection
    {
        public static IServiceCollection AddTodoDbContextServices(this IServiceCollection services)
        {
            services.AddOptions();
            services.AddHttpClient();

            services.AddScoped<AuditInterceptor>();
            services.AddDbContext<TodoDbContext>((sp, options) =>
            {
                var auditableInterceptor = sp.GetService<AuditInterceptor>();

                options.UseSqlServer("name=ConnectionStrings:ToDoListBase")
                .AddInterceptors(auditableInterceptor);

            });


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
            {

                options.Authority = "https://localhost:5443";
                options.Audience = "ShopApi";
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters.ValidateIssuer = true;
                options.TokenValidationParameters.ValidateAudience = true;
                options.TokenValidationParameters.ValidateIssuerSigningKey = true;

                //options.TokenValidationParameters = new TokenValidationParameters()
                //{
                //    NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"

                //};


                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = msg =>
                    {
                        var token = msg?.Request.Headers.Authorization.ToString();
                        string path = msg?.Request.Path ?? "";
                        if (!string.IsNullOrEmpty(token))

                        {
                            Console.WriteLine("Access token");
                            Console.WriteLine($"URL: {path}");
                            Console.WriteLine($"Token: {token}\r\n");
                        }
                        else
                        {
                            Console.WriteLine("Access token");
                            Console.WriteLine("URL: " + path);
                            Console.WriteLine("Token: No access token provided\r\n");
                        }
                        return Task.CompletedTask;
                    }
                    ,

                    OnTokenValidated = ctx =>
                    {
                        Console.WriteLine();
                        Console.WriteLine("Claims from the access token");
                        if (ctx?.Principal != null)
                        {
                            foreach (var claim in ctx.Principal.Claims)
                            {
                                Console.WriteLine($"{claim.Type} - {claim.Value}");
                            }
                        }
                        Console.WriteLine();
                        return Task.CompletedTask;
                    }
                };
            });


            services.AddAuthorization();

            services.AddCors(corsOptions =>
            {
                corsOptions.AddDefaultPolicy(corsPolicyBuilder =>
                {
                    corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            return services;
        }

        public static IServiceCollection AddCQRS(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceColection).Assembly));

            return services;
        }
    }
}
