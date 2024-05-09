using MediatR;

namespace TodoList.DAL.Commands.AddSubTask
{
    public class DeleteSubTaskCommand : IRequest<DeleteSubTaskCommandResult>
    {
        public Guid Id { get; set; }
    }
}
