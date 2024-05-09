using MediatR;

namespace TodoList.DAL.Queries.GetUserTask
{
    public class GetTaskQuery : IRequest<GetTaskQueryResult>
    {
        public Guid TaskId { get; set; }
    }
}
