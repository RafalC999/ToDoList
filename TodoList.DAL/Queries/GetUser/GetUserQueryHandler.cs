using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TodoList.DAL.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserQueryResult>
    {
        private readonly TodoDbContext _dbConext;

        public GetUserQueryHandler(TodoDbContext dbConext)
        {
            _dbConext = dbConext;
        }

        public async Task<GetUserQueryResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var output = new GetUserQueryResult();

            output.User = await _dbConext.Users.FirstOrDefaultAsync(x => x.Id == request.Id.ToString());

            return output;
        }
    }
}
