using Microsoft.AspNetCore.Components;
using TodoList.DAL.Entities;
using ToDoList.Client.Services.Interfaces;

namespace ToDoList.Client.Pages
{
    public partial class Tasks
    {
        [Inject] private IToDoService toDoService { get; set; }
        [Inject] private IUserService userService { get; set; }

        public ApplicationUser user { get; set; }

        protected override async Task OnInitializedAsync()
        {
            UserTasks = await toDoService.GetTaskListAsync();
        }
    }
}
