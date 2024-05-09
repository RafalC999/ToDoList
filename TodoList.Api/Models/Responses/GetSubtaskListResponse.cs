using TodoList.DAL.Entities;

namespace TodoList.Api.Models.Responses
{
    public class GetSubtaskListResponse
    {
        public GetSubtaskListResponse()
        {
            SubTasksList = new List<SubTask> { };
        }

        public List<SubTask> SubTasksList { get; set; }
    }
}
