using IdentityModel.Client;
using TodoList.DAL.Entities;
using ToDoList.Client.Services.Interfaces;

namespace ToDoList.Client.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        private readonly IJsonService _jsonService;
        private readonly HttpClient httpClient;

        public UserService(ITokenService tokenService, IJsonService jsonService, IConfiguration config)
        {
            _tokenService = tokenService;
            _jsonService = jsonService;
            _config = config;
            httpClient = new HttpClient();
        }

        public async Task SetToken()
        {
            var token = await _tokenService.GetToken("ShopApi.read");
            httpClient.SetBearerToken(token.AccessToken!);
        }


        public async Task<ApplicationUser> GetUser(string Id)
        {
            await SetToken();

            var user = new ApplicationUser
            {

            };

            var result = await httpClient.GetAsync((_config["apiUrl"] + $"/UserTasks/GetUser/?id={Id}"));

            if (result.IsSuccessStatusCode)
            {
                user = await _jsonService.DeserializeUserJson(result);

            }
            return user;
        }

    }
}
