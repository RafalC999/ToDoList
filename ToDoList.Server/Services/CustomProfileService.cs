using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TodoList.DAL.Entities;

namespace ToDoList.Server.Services
{
    public class CustomProfileService : ProfileService<ApplicationUser>
    {
        public CustomProfileService(UserManager<ApplicationUser> userManager,
                            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
                            : base(userManager, claimsFactory)
        {
        }

        public CustomProfileService(UserManager<ApplicationUser> userManager,
                                    IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
                                    ILogger<ProfileService<ApplicationUser>> logger)
                                    : base(userManager, claimsFactory, logger)
        {
        }


        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject?.GetSubjectId();

            if (sub == null)
            {
                throw new Exception("No sub claim present");
            }

            var user = await UserManager.FindByIdAsync(sub);
            if (user == null)
            {
                Logger?.LogWarning("No user found matching subject Id: {0}", sub);
                return;
            }

            var claimsPrincipal = await ClaimsFactory.CreateAsync(user);
            if (claimsPrincipal == null)
            {
                throw new Exception("ClaimsFactory failed to create a principal");
            }
            List<Claim> claims = new List<Claim>() { new Claim("langId", "en"), new Claim("tenantId", "123") };
            context.IssuedClaims = claims;
            context.RequestedClaimTypes = context.RequestedClaimTypes.Union(new[] { "email" }).ToList();
            context.AddRequestedClaims(claimsPrincipal.Claims);
        }

    }
}
