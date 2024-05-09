namespace ToDoList.Client.Pages
{
    public partial class TasksList
    {


        //private List<UserTask> UserTasks = new();
        //[Inject] private HttpClient HttpClient { get; set; }
        //[Inject] private IConfiguration Config { get; set; }
        //[Inject] private ITokenService TokenServices { get; set; }

        //protected override async Task OnInitializedAsync()
        //{
        //    var tokenResponse = await TokenServices.GetToken("ShopApi.read ShopApi.write");
        //    HttpClient.SetBearerToken(tokenResponse.AccessToken!);


        //    var result = await HttpClient.GetAsync(Config["apiUrl"] + "/UserTasks/GetUserTaskList");

        //    if (result.IsSuccessStatusCode)
        //    {
        //        var json = await result.Content.ReadAsStringAsync();
        //        JsonNode original = JsonObject.Parse(json);
        //        var newJson = original["userTasks"].ToJsonString();

        //        UserTasks = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserTask>>(newJson);
        //    }

        //    foreach (var taskk in UserTasks)
        //    {
        //        foreach (var subtask in taskk.SubTasks)
        //        {
        //            Jobs.Add(subtask);
        //        }
        //    }
        //}
    }
}
