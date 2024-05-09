using Duende.IdentityServer.Models;

namespace ToDoList.Server
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
           new List<IdentityResource>
           {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}

                }
           };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("ShopApi.read"),
                new ApiScope("ShopApi.write"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("ShopApi")
                {
                    Scopes = new List<string> {"ShopApi.read", "ShopApi.write"},
                    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                    UserClaims = new List<string> {"role"}

                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "rafcio.client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("ClientSecret1".Sha256())},
                    AllowedScopes = {"openid", "ShopApi.read", "ShopApi.write" },
                    AlwaysIncludeUserClaimsInIdToken = true,

                },

                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = {new Secret("ClientSecret1".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = {"https://localhost:5444/signin-oidc"},
                    FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                    PostLogoutRedirectUris = {"https://localhost:5444/signout-callback-oidc"},
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "ShopApi.read" },
                    RequirePkce = true,
                    RequireConsent = true,
                    AllowPlainTextPkce = false
                }
            };
    }
}


