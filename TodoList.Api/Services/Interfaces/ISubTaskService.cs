using TodoList.Api.Models.Requests;
using TodoList.Api.Models.Responses;

namespace TodoList.Api.Services.Interfaces
{
    public interface ISubTaskService
    {
        Task<AddSubTaskResponse> AddSubTask(AddSubTaskRequest request);
        Task<DeleteSubTaskResponse> DeleteSubTask(DeleteSubTaskRequest request);
        Task<GetSubtaskListResponse> GetSubtaskList(GetSubtaskListRequest request);
    }
}
