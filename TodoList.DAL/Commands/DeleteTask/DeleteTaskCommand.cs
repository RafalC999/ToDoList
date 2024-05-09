using MediatR;

namespace TodoList.DAL.Commands.DeleteTask
{
    public class DeleteTaskCommand : IRequest<DeleteTaskCommandResult>
    {
        public Guid Id { get; set; }
    }
}
