using TodoList.DAL.Entities;

namespace TodoList.DAL.Queries.GetUserTaskList
{
    public class GetUserTaskListQueryResult
    {
        public GetUserTaskListQueryResult()
        {
            list = new List<UserTask>();
        }

        public List<UserTask> list { get; set; }

    }
}
