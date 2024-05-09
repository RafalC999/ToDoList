using MediatR;
using TodoList.DAL.Entities;
using TodoList.DAL.Queries.GetSubtasksList;

namespace TodoList.DAL.Queries.GetSubtasks
{
    public class GetSubtaskListQuery : IRequest<GetSubtaskListQueryResult>
    {
        public string Name { get; set; }
        public bool IsDone { get; set; }

        public int Order { get; set; }
        public SubtaskStatus Status { get; set; }

        public DateTime Deadline { get; set; }
        public Guid UserTaskId { get; set; }

    }
}
