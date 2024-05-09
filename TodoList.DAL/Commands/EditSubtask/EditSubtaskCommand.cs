using MediatR;
using TodoList.DAL.Entities;

namespace TodoList.DAL.Commands.EditSubtask
{
    public class EditSubtaskCommand : IRequest<EditSubtaskCommandResult>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public int Order { get; set; }
        public SubtaskStatus Status { get; set; }
    }
}
