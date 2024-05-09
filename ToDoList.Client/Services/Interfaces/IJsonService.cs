using TodoList.DAL.Entities;

namespace ToDoList.Client.Services.Interfaces
{
    public interface IJsonService
    {
        Task<List<UserTask>> DeserializeJson(HttpResponseMessage? result);
        Task<List<SubTask>> DeserializeSubtaskJson(HttpResponseMessage? result);
        Task<ApplicationUser> DeserializeUserJson(HttpResponseMessage? result);
        Task<ByteArrayContent> SerializeJson(SubTask subtask);
        Task<ByteArrayContent> SerializeJson(UserTask task);

    }
}
