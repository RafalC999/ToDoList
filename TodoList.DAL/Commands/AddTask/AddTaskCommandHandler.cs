using MediatR;

namespace TodoList.DAL.Commands.AddTask
{
    public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, AddTaskCommandResult>
    {
        private readonly TodoDbContext _dbConext;

        public AddTaskCommandHandler(TodoDbContext dbConext)
        {
            _dbConext = dbConext;
        }

        public async Task<AddTaskCommandResult> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            var newTask = new Entities.UserTask()
            {
                Name = request.Name,
                Description = request.Description,
                Deadline = request.Deadline,

                CreatedById = request.CreatedById,
                ModifiedById = request.ModifiedById,
            };

            await _dbConext.UserTasks.AddAsync(newTask);
            await _dbConext.SaveChangesAsync();

            return new AddTaskCommandResult()
            {
                TaskId = newTask.Id
            };
        }
    }
}
