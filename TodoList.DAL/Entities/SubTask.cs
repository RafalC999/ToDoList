using System.ComponentModel.DataAnnotations.Schema;
using TodoList.DAL.Core.Entities;

namespace TodoList.DAL.Entities
{
    public class SubTask : BaseEntity
    {
        public string Name { get; set; }
        public bool IsDone { get; set; } = false;

        public int Order { get; set; }
        public SubtaskStatus Status { get; set; } = SubtaskStatus.ToDo;

        public DateTime Deadline { get; set; }

        [ForeignKey("UserTask")]
        public Guid UserTaskId { get; set; }
        public UserTask UserTask { get; set; }
    }

    public enum SubtaskStatus
    {
        ToDo,
        InProgress,
        Completed
    }
}
