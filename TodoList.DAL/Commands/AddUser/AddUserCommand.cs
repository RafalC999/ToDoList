using MediatR;

namespace TodoList.DAL.Commands.AddUser
{
    public class AddUserCommand : IRequest<AddUserCommandResult>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
