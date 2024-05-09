using BlazorStrap;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using TodoList.DAL;
using ToDoList.Client.Services;
using ToDoList.Client.Services.Implementation;
using ToDoList.Client.Services.Interfaces;
using ToDoList.Client.Shared.Calendar;

namespace ToDoList.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpClient();

            builder.Services.AddBlazorStrap();
            builder.Services.AddBlazorContextMenu();


            builder.Services.AddDbContext<TodoDbContext>();

            builder.Services.Configure<IdentityServerSettings>(builder.Configuration.GetSection("IdentityServerSettings"));

            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IJsonService, JsonService>();
            builder.Services.AddScoped<IToDoService, ToDoService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<SubtaskModal>();


            // builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    //options.Cookie.SameSite = SameSiteMode.Lax;
                    options.Events.OnSigningIn = ctx =>
                    {
                        ctx.Properties.IsPersistent = true;
                        Console.WriteLine();
                        Console.WriteLine("Claims received by the Cookie handler");
                        foreach (var claim in ctx.Principal.Claims)
                        {
                            Console.WriteLine($"{claim.Type} - {claim.Value}");
                        }
                        Console.WriteLine();

                        return Task.CompletedTask;
                    };

                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.SignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.Authority = builder.Configuration["InteractiveServiceSettings:AuthorityUrl"];
                options.ClientId = builder.Configuration["InteractiveServiceSettings:ClientId"];
                options.ClientSecret = builder.Configuration["InteractiveServiceSettings:ClientSecret"];

                options.ResponseType = "code";
                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("ShopApi.read");


                options.MapInboundClaims = false;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.SaveTokens = true;



                options.TokenValidationParameters = new()
                {
                    NameClaimType = JwtClaimTypes.Subject,
                    RoleClaimType = JwtClaimTypes.Role,

                };


            });

            builder.Services.AddAuthorization();



            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();



            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");


            app.Run();
        }
    }
}