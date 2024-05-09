using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TodoList.DAL.Queries.GetUserTask
{
    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, GetTaskQueryResult>
    {
        private readonly TodoDbContext _dbConext;

        public GetTaskQueryHandler(TodoDbContext dbConext)
        {
            _dbConext = dbConext;
        }

        public async Task<GetTaskQueryResult> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var output = new GetTaskQueryResult();

            output.Task = await _dbConext.UserTasks.FirstOrDefaultAsync(x => x.Id == request.TaskId);

            return output;
        }
    }
}
