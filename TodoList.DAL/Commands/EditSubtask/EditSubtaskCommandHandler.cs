using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.DAL.Entities;

namespace TodoList.DAL.Commands.EditSubtask
{
    public class EditSubtaskCommandHandler : IRequestHandler<EditSubtaskCommand, EditSubtaskCommandResult>
    {
        private readonly TodoDbContext _todoDbContext;
        public EditSubtaskCommandHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<EditSubtaskCommandResult> Handle(EditSubtaskCommand request, CancellationToken cancellationToken)
        {
            SubTask subtask = await _todoDbContext.SubTasks.FirstOrDefaultAsync(t => t.Id == request.Id);
            subtask.Name = request.Name;
            subtask.IsDone = request.IsDone;
            subtask.Order = request.Order;
            subtask.Status = request.Status;
            if (subtask != null)
            {
                _todoDbContext.Entry(subtask).State = EntityState.Modified;
                await _todoDbContext.SaveChangesAsync();
            }


            return new EditSubtaskCommandResult() { Name = request.Name };
        }
    }
}
