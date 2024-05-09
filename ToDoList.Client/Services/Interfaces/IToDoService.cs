using TodoList.DAL.Entities;

namespace ToDoList.Client.Services.Interfaces
{
    public interface IToDoService
    {
        Task<List<UserTask>> GetTaskListAsync();
        Task<List<SubTask>> GetSubtaskListAsync();
        Task OnDeleteSubtask(SubTask request);
        Task OnEditSubtask(SubTask request);
        Task OnPostSubtask(UserTask request, SubTask inputSubtask);
        Task OnPostTask(UserTask inputTask);
        Task OnDeleteTask(UserTask request);
        Task OnEditTask(UserTask request);
    }
}
