using MediatR;

namespace TodoList.DAL.Queries.GetUserTaskList
{
    public class GetUserTaskListQuery : IRequest<GetUserTaskListQueryResult>
    {
        public string Name { get; set; }

        public string? Description { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }

    }
}
