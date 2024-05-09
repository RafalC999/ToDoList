using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.DAL.Entities;

namespace TodoList.DAL.Queries.GetUserTaskList
{
    public class GetUserTaskListQueryHandler : IRequestHandler<GetUserTaskListQuery, GetUserTaskListQueryResult>
    {
        private readonly TodoDbContext _dbConext;

        public GetUserTaskListQueryHandler(TodoDbContext dbConext)
        {
            _dbConext = dbConext;
        }

        public async Task<GetUserTaskListQueryResult> Handle(GetUserTaskListQuery request, CancellationToken cancellationToken)
        {
            var output = new GetUserTaskListQueryResult();

            var query = _dbConext.UserTasks.Include(i => i.SubTasks);


            output.list = await (from task in query
                                 select new UserTask()
                                 {
                                     Name = task.Name,
                                     Id = task.Id,
                                     CreatedById = task.CreatedById,
                                     CreateOn = task.CreateOn,
                                     ModifiedById = task.ModifiedById,
                                     ModifiedOn = task.ModifiedOn,
                                     SubTasks = task.SubTasks,
                                     Deadline = task.Deadline,
                                     Description = task.Description


                                 }).ToListAsync();

            return output;
        }
    }
}
