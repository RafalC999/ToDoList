using MediatR;

namespace TodoList.DAL.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, DeleteTaskCommandResult>
    {
        private readonly TodoDbContext _todoDbContext;
        public DeleteTaskCommandHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }
        public async Task<DeleteTaskCommandResult> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var item = _todoDbContext.UserTasks.FirstOrDefault(i => i.Id == request.Id);
            var subItems = _todoDbContext.SubTasks.Where(x => x.UserTaskId == request.Id);
            _todoDbContext.SubTasks.RemoveRange(subItems);
            _todoDbContext.UserTasks.Remove(item);
            await _todoDbContext.SaveChangesAsync();

            return new DeleteTaskCommandResult
            {
                Id = item.Id,
            };
        }
    }
}
