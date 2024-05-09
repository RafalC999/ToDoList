using TodoList.DAL.Entities;

namespace TodoList.Api.Models.Requests
{
    public class EditSubtaskRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public int Order { get; set; }
        public SubtaskStatus Status { get; set; }
    }
}
