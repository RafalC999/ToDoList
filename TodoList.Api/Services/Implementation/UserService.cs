using AutoMapper;
using MediatR;
using TodoList.Api.Models.Responses;
using TodoList.Api.Services.Interfaces;
using TodoList.DAL.Queries.GetUser;
//using TodoList.DAL.Queries.GetUserToken;
//using TodoList.DAL.Queries.LogInUser;

namespace TodoList.Api.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<GetUserResponse> GetUser(Guid id)
        {

            var result = await _mediator.Send(new GetUserQuery() { Id = id });


            return _mapper.Map<GetUserResponse>(result);
        }
    }
}
