using MediatR;

namespace TodoList.DAL.Commands.AddTask
{
    public class AddTaskCommand : IRequest<AddTaskCommandResult>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime Deadline { get; set; }

        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
    }
}
