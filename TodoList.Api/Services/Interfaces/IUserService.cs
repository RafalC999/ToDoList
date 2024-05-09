//using TodoList.DAL.Queries.GetUserToken;
//using TodoList.DAL.Queries.LogInUser;

using TodoList.Api.Models.Responses;

namespace TodoList.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<GetUserResponse> GetUser(Guid id);
    }
}
