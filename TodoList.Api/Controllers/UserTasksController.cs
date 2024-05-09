using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Models.Requests;
using TodoList.Api.Models.Responses;
using TodoList.Api.Services.Interfaces;

namespace TodoList.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class UserTasksController : ControllerBase
    {
        private readonly IUserTasksService _userTasksService;
        private readonly ISubTaskService _subTaskService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;


        public UserTasksController(IUserTasksService userTasksService, ISubTaskService subTaskService, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _userTasksService = userTasksService;
            _subTaskService = subTaskService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = new HttpClient();
            _userService = userService;
        }

        [HttpGet]
        public async Task<GetUserResponse> GetUser(Guid Id)
        {
            return await _userService.GetUser(Id);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<GetAllTasksResponse> GetAllTasks([FromQuery] GetAllTasksRequest request)
        {

            return await _userTasksService.GetAllTasks(request);
        }

        [HttpGet]
        public async Task<GetTaskResponse> GetTask(Guid taskId)
        {
            return await _userTasksService.GetTask(taskId);
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<GetUserTaskListResponse> GetUserTaskList([FromQuery] GetUserTaskListRequest request)
        {
            var tmp = _httpContextAccessor.HttpContext.User;


            return await _userTasksService.GetUserTaskList(request);
        }

        [HttpGet]
        public async Task<GetSubtaskListResponse> GetSubtaskList([FromQuery] GetSubtaskListRequest request)
        {
            var tmp = _httpContextAccessor.HttpContext.User;

            return await _subTaskService.GetSubtaskList(request);
        }

        [HttpPost]
        public async Task<AddTaskResponse> AddTask(AddTaskRequest request)
        {
            var tmp = _httpContextAccessor.HttpContext.User;
            return await _userTasksService.AddTask(request);
        }

        [HttpPost]
        public async Task<AddSubTaskResponse> AddSubTask(AddSubTaskRequest request)
        {
            var tmp = _httpContextAccessor.HttpContext.User;
            return await _subTaskService.AddSubTask(request);
        }

        [HttpDelete]
        public async Task<DeleteSubTaskResponse> DeleteSubTask(DeleteSubTaskRequest request)
        {
            var tmp = _httpContextAccessor.HttpContext.User;
            return await _subTaskService.DeleteSubTask(request);
        }

        [HttpDelete]
        public async Task<DeleteTaskResponse> DeleteTask(DeleteTaskRequest request)
        {
            var tmp = _httpContextAccessor.HttpContext.User;
            return await _userTasksService.DeleteTask(request);
        }

        [HttpPut]
        public async Task<EditTaskResponse> EditTask(EditTaskRequest request)
        {
            return await _userTasksService.EditTask(request);
        }

        [HttpPut]

        public async Task<EditSubtaskResponse> EditSubtask(EditSubtaskRequest request)
        {
            return await _userTasksService.EditSubtask(request);
        }
    }
}