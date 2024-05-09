using Microsoft.AspNetCore.Components;


namespace ToDoList.Client.Shared
{
    public partial class RedirectToLogIn
    {
        [Inject] public NavigationManager Navigation { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Navigation.NavigateTo($"/login?redirectUri={Uri.EscapeDataString(Navigation.Uri)}", true);
        }
    }
}
