namespace TodoList.Api.Models.Requests
{
    public class AddSubTaskRequest
    {
        public string Name { get; set; }

        public Guid UserTaskId { get; set; }
        public DateTime Deadline { get; set; }

    }
}
