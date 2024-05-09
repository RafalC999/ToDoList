using TodoList.DAL.Entities;

namespace TodoList.Api.Models.Responses
{
    public class GetUserTaskListResponse
    {
        public GetUserTaskListResponse()
        {
            UserTasks = new List<UserTask>();
        }
        public List<UserTask> UserTasks { get; set; }
    }
}
