using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.DAL.Entities;

namespace TodoList.DAL.Commands.EditTask
{
    public class EditTaskCommandHandler : IRequestHandler<EditTaskCommand, EditTaskCommandResult>
    {
        private readonly TodoDbContext _todoDbContext;
        public EditTaskCommandHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<EditTaskCommandResult> Handle(EditTaskCommand request, CancellationToken cancellationToken)
        {
            UserTask task = await _todoDbContext.UserTasks.FirstOrDefaultAsync(t => t.Id == request.Id);
            task.Name = request.Name;
            task.Deadline = request.Deadline;
            task.Description = request.Description;

            if (task != null)
            {
                _todoDbContext.Entry(task).State = EntityState.Modified;
                await _todoDbContext.SaveChangesAsync();
            }


            return new EditTaskCommandResult() { Name = request.Name };
        }
    }
}
