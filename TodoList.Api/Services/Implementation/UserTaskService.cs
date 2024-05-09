using AutoMapper;
using MediatR;
using TodoList.Api.Models.Requests;
using TodoList.Api.Models.Responses;
using TodoList.Api.Services.Interfaces;
using TodoList.DAL.Commands.AddTask;
using TodoList.DAL.Commands.DeleteTask;
using TodoList.DAL.Commands.EditSubtask;
using TodoList.DAL.Commands.EditTask;
using TodoList.DAL.Models;
using TodoList.DAL.Queries.GetUserTask;
using TodoList.DAL.Queries.GetUserTaskList;
using TodoList.DAL.Queries.GetUserTasks;

namespace TodoList.Api.Services.Implementation
{
    public class UserTaskService : IUserTasksService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserTaskService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<GetTaskResponse> GetTask(Guid taskId)
        {
            var result = await _mediator.Send(new GetTaskQuery() { TaskId = taskId });

            return _mapper.Map<GetTaskResponse>(result.Task);
        }

        public async Task<GetAllTasksResponse> GetAllTasks(GetAllTasksRequest request)
        {
            var query = new GetAllUserTasksQuery()
            {
                Paging = _mapper.Map<BasePage>(request)
            };

            var result = await _mediator.Send(query);

            return _mapper.Map<GetAllTasksResponse>(result);
        }

        public async Task<GetUserTaskListResponse> GetUserTaskList(GetUserTaskListRequest request)
        {
            var query = new GetUserTaskListQuery()
            {

            };

            var result = await _mediator.Send(query);


            return new GetUserTaskListResponse()
            {
                UserTasks = result.list
            };
        }

        public async Task<AddTaskResponse> AddTask(AddTaskRequest request)
        {
            var command = new AddTaskCommand()
            {
                Name = request.Name,
                Description = request.Description,
                Deadline = request.Deadline,

                CreatedById = request.CreatedById,
                ModifiedById = request.ModifiedById,
            };

            var result = await _mediator.Send(command);

            return new AddTaskResponse()
            {
                Id = result.TaskId
            };
        }

        public async Task<EditTaskResponse> EditTask(EditTaskRequest request)
        {
            var command = new EditTaskCommand()
            {
                Id = request.Id,
                Name = request.Name
            };

            var result = await _mediator.Send(command);

            return new EditTaskResponse()
            {
                Name = result.Name,
            };
        }

        public async Task<EditSubtaskResponse> EditSubtask(EditSubtaskRequest request)
        {
            var command = new EditSubtaskCommand()
            {
                Id = request.Id,
                Name = request.Name,
                IsDone = request.IsDone,
                Order = request.Order,
                Status = request.Status
            };

            var result = await _mediator.Send(command);

            return new EditSubtaskResponse()
            {
                Name = result.Name,
                IsDone = result.IsDone,
            };
        }

        public async Task<DeleteTaskResponse> DeleteTask(DeleteTaskRequest request)
        {
            var command = new DeleteTaskCommand()
            {
                Id = request.Id

            };

            var result = await _mediator.Send(command);

            return new DeleteTaskResponse()
            {
                Id = result.Id
            };
        }

    }
}
