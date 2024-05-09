using TodoList.Api.Models.Requests;
using TodoList.Api.Models.Responses;

namespace TodoList.Api.Services.Interfaces
{
    public interface IUserTasksService
    {
        Task<AddTaskResponse> AddTask(AddTaskRequest request);
        Task<DeleteTaskResponse> DeleteTask(DeleteTaskRequest request);
        Task<EditSubtaskResponse> EditSubtask(EditSubtaskRequest request);
        Task<EditTaskResponse> EditTask(EditTaskRequest request);
        Task<GetAllTasksResponse> GetAllTasks(GetAllTasksRequest request);
        Task<GetTaskResponse> GetTask(Guid taskId);
        Task<GetUserTaskListResponse> GetUserTaskList(GetUserTaskListRequest request);
    }
}
