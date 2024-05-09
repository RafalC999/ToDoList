using MediatR;

namespace TodoList.DAL.Commands.AddSubTask
{
    public class DeleteSubTaskCommandHandler : IRequestHandler<DeleteSubTaskCommand, DeleteSubTaskCommandResult>
    {
        private readonly TodoDbContext _todoDbContext;
        public DeleteSubTaskCommandHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<DeleteSubTaskCommandResult> Handle(DeleteSubTaskCommand request, CancellationToken cancellationToken)
        {

            var item = _todoDbContext.SubTasks.FirstOrDefault(i => i.Id == request.Id);
            _todoDbContext.SubTasks.Remove(item);
            await _todoDbContext.SaveChangesAsync();

            return new DeleteSubTaskCommandResult
            {
                Id = item.Id,
            };
        }
    }
}
