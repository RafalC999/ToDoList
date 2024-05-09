using TodoList.DAL.Core.Entities;

namespace TodoList.DAL.Entities
{
    public class UserTask : BaseEntity
    {

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime Deadline { get; set; }

        public virtual ICollection<SubTask> SubTasks { get; set; }
    }
}
