using MediatR;

namespace TodoList.DAL.Queries.GetUser
{
    public class GetUserQuery : IRequest<GetUserQueryResult>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string email { get; set; }
    }
}
