using AutoMapper;
using MediatR;
using TodoList.Api.Models.Requests;
using TodoList.Api.Models.Responses;
using TodoList.Api.Services.Interfaces;
using TodoList.DAL.Commands.AddSubTask;
using TodoList.DAL.Queries.GetSubtasks;

namespace TodoList.Api.Services.Implementation
{
    public class SubTaskService : ISubTaskService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SubTaskService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<AddSubTaskResponse> AddSubTask(AddSubTaskRequest request)
        {
            var command = new AddSubTaskCommand()
            {
                Name = request.Name,
                UserTaskId = request.UserTaskId,
                Deadline = request.Deadline

            };

            var result = await _mediator.Send(command);

            return new AddSubTaskResponse()
            {
                Id = result.SubtaskId
            };
        }

        public async Task<GetSubtaskListResponse> GetSubtaskList(GetSubtaskListRequest request)
        {
            var query = new GetSubtaskListQuery()
            {

            };

            var result = await _mediator.Send(query);


            return new GetSubtaskListResponse()
            {
                SubTasksList = result.SubtaskList
            };
        }

        public async Task<DeleteSubTaskResponse> DeleteSubTask(DeleteSubTaskRequest request)
        {
            var command = new DeleteSubTaskCommand()
            {
                Id = request.Id

            };

            var result = await _mediator.Send(command);

            return new DeleteSubTaskResponse()
            {
                Id = result.Id
            };
        }
    }
}
