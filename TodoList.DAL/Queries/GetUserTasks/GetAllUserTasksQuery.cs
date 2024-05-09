using MediatR;
using TodoList.DAL.Models;

namespace TodoList.DAL.Queries.GetUserTasks
{
    public class GetAllUserTasksQuery : IRequest<GetAllUserTasksQueryResult>
    {
        public BasePage Paging { get; set; }
    }
}
