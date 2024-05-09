using IdentityModel.Client;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Text;
using TodoList.DAL.Entities;
using ToDoList.Client.Services;
using ToDoList.Client.Services.Interfaces;

namespace ToDoList.Client.Pages
{
    public partial class TESTS
    {
        public List<UserTask> UserTasks = new();

        [Inject] private HttpClient HttpClient { get; set; }
        [Inject] private IConfiguration Config { get; set; }
        [Inject] private ITokenService TokenServices { get; set; }
        [Inject] private IJsonService jsonService { get; set; }
        [Inject] private IToDoService toDoService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            UserTasks = await toDoService.GetTaskListAsync();
        }

        public async Task OnPostTask()
        {
            var tokenResponse = await TokenServices.GetToken("ShopApi.read");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);


            if (InputTask.Name != null)
            {
                var task = new UserTask
                {
                    Name = InputTask.Name,
                };
                var byteContent = await jsonService.SerializeJson(task);

                var result = await HttpClient.PostAsync(Config["apiUrl"] + "/UserTasks/AddTask", byteContent);
            }
        }
        //
        public async Task OnPostSubtask(UserTask request)
        {
            var tokenResponse = await TokenServices.GetToken("ShopApi.read ShopApi.write");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            if (InputSubtask.Name != null)
            {
                var subtask = new SubTask
                {
                    Name = InputSubtask.Name,
                    UserTaskId = request.Id,
                    Status = SubtaskStatus.ToDo
                };
                var byteContent = await jsonService.SerializeJson(subtask);

                var result = await HttpClient.PostAsync(Config["apiUrl"] + "/UserTasks/AddSubTask", byteContent);
                request.SubTasks.Add(subtask);
            }
            InputSubtask.Name = null;
            // await OnInitializedAsync();
            StateHasChanged();
        }
        //
        public async void OnEditSubtask(SubTask request)
        {
            var tokenResponse = await TokenServices.GetToken("ShopApi.read");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            if (request != null)
            {
                var subtask = new SubTask
                {
                    Id = request.Id,
                    Name = request.Name,
                    IsDone = request.IsDone,
                    Order = request.Order,
                };
                var byteContent = await jsonService.SerializeJson(subtask);

                var result = await HttpClient.PutAsync(Config["apiUrl"] + "/UserTasks/EditSubtask", byteContent);
            }
        }
        //
        public async Task OnDeleteSubtask(SubTask request)
        {
            var tokenResponse = await TokenServices.GetToken("ShopApi.read ShopApi.write");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            if (request != null)
            {
                var subtask = new SubTask
                {
                    Id = request.Id,
                };

                var content = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri((Config["apiUrl"] + "/UserTasks/DeleteSubTask"))
                };
                content.Content = new StringContent(JsonConvert.SerializeObject(subtask), Encoding.UTF8, "application/json");
                await HttpClient.SendAsync(content);
            }
            await OnInitializedAsync();
            StateHasChanged();
        }
    }
}
