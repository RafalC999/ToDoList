namespace TodoList.Api.Models.Requests
{
    public class AddTaskRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime Deadline { get; set; }

        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
    }
}
