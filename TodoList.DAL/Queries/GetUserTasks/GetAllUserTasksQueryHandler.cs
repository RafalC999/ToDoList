using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TodoList.DAL.Queries.GetUserTasks
{
    public class GetAllUserTasksQueryHandler : IRequestHandler<GetAllUserTasksQuery, GetAllUserTasksQueryResult>
    {
        private readonly TodoDbContext _dbConext;

        public GetAllUserTasksQueryHandler(TodoDbContext dbConext)
        {
            _dbConext = dbConext;
        }

        public async Task<GetAllUserTasksQueryResult> Handle(GetAllUserTasksQuery request, CancellationToken cancellationToken)
        {
            var output = new GetAllUserTasksQueryResult();

            var query = _dbConext.UserTasks;

            output.UserTasks = await query
                .Skip(request.Paging.Skip)
                .Take(request.Paging.Take)
                .ToListAsync();

            output.Total = await query.CountAsync();
            return output;
        }
    }
}
