using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.DAL.Entities;
using TodoList.DAL.Queries.GetSubtasks;

namespace TodoList.DAL.Queries.GetSubtasksList
{
    public class GetSubtaskListQueryHandler : IRequestHandler<GetSubtaskListQuery, GetSubtaskListQueryResult>
    {
        private readonly TodoDbContext _dbConext;

        public GetSubtaskListQueryHandler(TodoDbContext dbConext)
        {
            _dbConext = dbConext;
        }

        public async Task<GetSubtaskListQueryResult> Handle(GetSubtaskListQuery request, CancellationToken cancellationToken)
        {
            var output = new GetSubtaskListQueryResult();

            var query = _dbConext.SubTasks;

            output.SubtaskList = await (from subtask in query
                                        select new SubTask()
                                        {
                                            Name = subtask.Name,
                                            CreatedBy = subtask.CreatedBy,
                                            CreateOn = subtask.CreateOn,
                                            ModifiedBy = subtask.ModifiedBy,
                                            ModifiedOn = subtask.ModifiedOn,
                                            Id = subtask.Id,
                                            IsDone = subtask.IsDone,
                                            Deadline = subtask.Deadline,
                                            Order = subtask.Order,
                                            UserTaskId = subtask.UserTaskId,
                                            Status = subtask.Status
                                        }).ToListAsync();
            return output;
        }
    }
}
