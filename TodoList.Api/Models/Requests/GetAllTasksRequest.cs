namespace TodoList.Api.Models.Requests
{
    public class GetAllTasksRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
