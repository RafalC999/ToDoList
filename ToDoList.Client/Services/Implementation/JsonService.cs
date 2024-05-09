using Newtonsoft.Json;
using System.Text.Json.Nodes;
using TodoList.DAL.Entities;
using ToDoList.Client.Services.Interfaces;

namespace ToDoList.Client.Services.Implementation
{
    public class JsonService : IJsonService
    {
        private List<UserTask> UserTasks = new();
        public async Task<List<UserTask>> DeserializeJson(HttpResponseMessage? result)
        {
            if (result != null)
            {
                var json = await result.Content.ReadAsStringAsync();
                JsonNode original = JsonObject.Parse(json);
                var newJson = original["userTasks"].ToJsonString();

                return JsonConvert.DeserializeObject<List<UserTask>>(newJson)!;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<SubTask>> DeserializeSubtaskJson(HttpResponseMessage? result)
        {
            if (result != null)
            {
                var json = await result.Content.ReadAsStringAsync();
                JsonNode original = JsonObject.Parse(json);
                var newJson = original["subTasksList"].ToJsonString();

                return JsonConvert.DeserializeObject<List<SubTask>>(newJson)!;
            }
            else
            {
                return null;
            }
        }

        public async Task<ApplicationUser> DeserializeUserJson(HttpResponseMessage? result)
        {
            if (result != null)
            {
                var json = await result.Content.ReadAsStringAsync();
                JsonNode original = JsonObject.Parse(json);
                var newJson = original["user"].ToJsonString();

                return JsonConvert.DeserializeObject<ApplicationUser>(newJson)!;
            }
            else
            {
                return null;
            }
        }

        public Task<ByteArrayContent> SerializeJson(SubTask subtask)
        {
            var content = JsonConvert.SerializeObject(subtask);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return Task.FromResult(byteContent);
        }

        public Task<ByteArrayContent> SerializeJson(UserTask task)
        {
            var content = JsonConvert.SerializeObject(task);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return Task.FromResult(byteContent);
        }


    }
}
