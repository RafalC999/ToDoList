using IdentityModel.Client;
using Newtonsoft.Json;
using System.Text;
using TodoList.DAL.Entities;
using ToDoList.Client.Services.Interfaces;

namespace ToDoList.Client.Services.Implementation
{
    public class ToDoService : IToDoService
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        private readonly IJsonService _jsonService;
        private readonly HttpClient httpClient;
        private string userId { get; set; }
        public ToDoService(ITokenService tokenService, IJsonService jsonService, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _tokenService = tokenService;
            _jsonService = jsonService;
            _config = config;
            httpClient = new HttpClient();
            userId = httpContextAccessor.HttpContext.User.Identity.Name;
        }

        public async Task SetToken()
        {
            var token = await _tokenService.GetToken("ShopApi.read");
            httpClient.SetBearerToken(token.AccessToken!);
        }

        public async Task<List<UserTask>> GetTaskListAsync()
        {
            await SetToken();

            List<UserTask> UserTasks = new();
            var result = await httpClient.GetAsync(_config["apiUrl"] + "/UserTasks/GetUserTaskList");

            if (result.IsSuccessStatusCode)
            {
                UserTasks = await _jsonService.DeserializeJson(result);
            }
            return UserTasks;
        }

        public async Task<List<SubTask>> GetSubtaskListAsync()
        {
            await SetToken();

            List<SubTask> Subtasks = new();
            var result = await httpClient.GetAsync(_config["apiUrl"] + "/UserTasks/GetSubtaskList");

            if (result.IsSuccessStatusCode)
            {
                Subtasks = await _jsonService.DeserializeSubtaskJson(result);
            }
            return Subtasks;
        }

        public async Task OnPostTask(UserTask inputTask)
        {
            await SetToken();

            if (inputTask.Name != null)
            {
                var task = new UserTask
                {
                    Name = inputTask.Name,
                    Deadline = inputTask.Deadline,
                    Description = inputTask.Description,
                    CreatedById = userId,
                    ModifiedById = userId
                };
                var byteContent = await _jsonService.SerializeJson(task);

                var result = await httpClient.PostAsync(_config["apiUrl"] + "/UserTasks/AddTask", byteContent);
            }
        }

        public async Task OnPostSubtask(UserTask request, SubTask inputSubtask)
        {
            await SetToken();

            if (inputSubtask.Name != null)
            {
                var subtask = new SubTask
                {
                    Name = inputSubtask.Name,
                    UserTaskId = request.Id,
                    Status = SubtaskStatus.ToDo,
                    Deadline = inputSubtask.Deadline,

                    CreatedById = userId,
                    ModifiedById = userId

                };
                var byteContent = await _jsonService.SerializeJson(subtask);

                var result = await httpClient.PostAsync(_config["apiUrl"] + "/UserTasks/AddSubTask", byteContent);
            }
        }

        public async Task OnEditSubtask(SubTask request)
        {
            if (request != null)
            {
                var subtask = new SubTask
                {
                    Id = request.Id,
                    Name = request.Name,
                    IsDone = request.IsDone,
                    Order = request.Order,
                    Status = request.Status,

                    ModifiedById = userId
                };
                var byteContent = await _jsonService.SerializeJson(subtask);

                var result = await httpClient.PutAsync(_config["apiUrl"] + "/UserTasks/EditSubtask", byteContent);
            }
        }

        public async Task OnEditTask(UserTask request)
        {
            if (request != null)
            {
                var task = new UserTask
                {
                    Id = request.Id,
                    Name = request.Name,
                    Deadline = request.Deadline,
                    Description = request.Description,
                    ModifiedById = userId
                };
                var byteContent = await _jsonService.SerializeJson(task);

                var result = await httpClient.PutAsync(_config["apiUrl"] + "/UserTasks/EditTask", byteContent);
            }
        }

        public async Task OnDeleteSubtask(SubTask request)
        {
            await SetToken();

            if (request != null)
            {
                var subtask = new SubTask
                {
                    Id = request.Id,
                };

                var content = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri((_config["apiUrl"] + "/UserTasks/DeleteSubTask"))
                };
                content.Content = new StringContent(JsonConvert.SerializeObject(subtask), Encoding.UTF8, "application/json");
                await httpClient.SendAsync(content);
            }
        }


        public async Task OnDeleteTask(UserTask request)
        {
            await SetToken();

            if (request != null)
            {
                var task = new UserTask
                {
                    Id = request.Id,
                };

                var content = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri((_config["apiUrl"] + "/UserTasks/DeleteTask"))
                };
                content.Content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
                await httpClient.SendAsync(content);
            }
        }
    }
}
