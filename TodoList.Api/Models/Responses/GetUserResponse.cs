using TodoList.DAL.Entities;

namespace TodoList.Api.Models.Responses
{
    public class GetUserResponse
    {
        public GetUserResponse()
        {
            User = new ApplicationUser();
        }

        public ApplicationUser User { get; set; }
    }
}
