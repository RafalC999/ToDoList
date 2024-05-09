using TodoList.DAL.Entities;

namespace TodoList.Api.Models.Requests
{
    public class GetAllTasksResponse
    {
        public GetAllTasksResponse()
        {
            UserTasks = new List<UserTask>();
        }

        public List<UserTask> UserTasks { get; set; }
        public int Total { get; set; }
    }
}
