using TodoList.DAL.Entities;

namespace TodoList.DAL.Queries.GetSubtasksList
{
    public class GetSubtaskListQueryResult
    {
        public GetSubtaskListQueryResult()
        {
            SubtaskList = new List<SubTask> { };
        }

        public List<SubTask> SubtaskList { get; set; }
    }
}
