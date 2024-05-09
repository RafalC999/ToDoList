using MediatR;
using TodoList.DAL.Entities;

namespace TodoList.DAL.Commands.AddSubTask
{
    public class AddSubTaskCommandHandler : IRequestHandler<AddSubTaskCommand, AddSubTaskCommandResult>
    {
        private readonly TodoDbContext _todoDbContext;
        public AddSubTaskCommandHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<AddSubTaskCommandResult> Handle(AddSubTaskCommand request, CancellationToken cancellationToken)
        {
            var lastOrderNumber = _todoDbContext.SubTasks.Where(i => i.UserTaskId == request.UserTaskId);
            int nextOrderNumber;

            if (lastOrderNumber.Any())
            {
                nextOrderNumber = lastOrderNumber.Max(i => i.Order) + 1;
            }
            else
            {
                nextOrderNumber = 1;
            }

            var newSubtask = new SubTask()
            {
                Name = request.Name,
                UserTaskId = request.UserTaskId,
                Status = SubtaskStatus.ToDo,
                Order = nextOrderNumber,
                Deadline = request.Deadline
            };

            await _todoDbContext.SubTasks.AddAsync(newSubtask);
            await _todoDbContext.SaveChangesAsync();

            return new AddSubTaskCommandResult
            {
                SubtaskId = newSubtask.Id,
            };
        }
    }
}
