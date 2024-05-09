using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace ToDoList.Client.Services
{
    public class TokenService : ITokenService
    {
        public readonly IOptions<IdentityServerSettings> identityServerSettings;
        public readonly DiscoveryDocumentResponse discoveryDocumentResponse;
        private readonly HttpClient httpClient;
        public readonly IHttpContextAccessor _httpContext;
        public TokenService(IOptions<IdentityServerSettings> identityServerSettings, IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;

            this.identityServerSettings = identityServerSettings;
            httpClient = new HttpClient();
            discoveryDocumentResponse = httpClient
            .GetDiscoveryDocumentAsync(this.identityServerSettings.Value.DiscoveryUrl).Result;

            if (discoveryDocumentResponse.IsError)
            {
                throw new Exception("Unable to get discovery document",
                    discoveryDocumentResponse.Exception);
            }
        }


        public async Task<IdentityModel.Client.TokenResponse> GetToken(string scope)
        {
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocumentResponse.TokenEndpoint,

                ClientId = identityServerSettings.Value.ClientName,
                ClientSecret = identityServerSettings.Value.ClientPassword,

                Scope = scope,
            });

            if (tokenResponse.IsError)
            {
                throw new Exception("Unable to get token", tokenResponse.Exception);
            }
            return tokenResponse;
        }
    }
}
