using TodoList.DAL.Entities;

namespace TodoList.DAL.Queries.GetUserTasks
{
    public class GetAllUserTasksQueryResult
    {
        public GetAllUserTasksQueryResult()
        {
            UserTasks = new List<UserTask>();
        }

        public List<UserTask> UserTasks { get; set; }
        public int Total { get; set; }
    }
}
