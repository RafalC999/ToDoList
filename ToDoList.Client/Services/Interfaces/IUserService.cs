using TodoList.DAL.Entities;

namespace ToDoList.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUser(string Id);
    }
}
