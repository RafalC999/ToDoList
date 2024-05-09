namespace TodoList.Api.Models.Requests
{
    public class EditTaskRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
