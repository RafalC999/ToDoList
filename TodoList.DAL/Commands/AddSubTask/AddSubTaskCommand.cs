using MediatR;

namespace TodoList.DAL.Commands.AddSubTask
{
    public class AddSubTaskCommand : IRequest<AddSubTaskCommandResult>
    {
        public string Name { get; set; }

        public Guid UserTaskId { get; set; }

        public DateTime Deadline { get; set; }
    }
}
