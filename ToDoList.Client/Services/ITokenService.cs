using IdentityModel.Client;

namespace ToDoList.Client.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
