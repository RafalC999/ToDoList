using MediatR;

namespace TodoList.DAL.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, AddUserCommandResult>
    {
        private readonly TodoDbContext _dbConext;

        public AddUserCommandHandler(TodoDbContext dbConext)
        {
            _dbConext = dbConext;
        }

        public async Task<AddUserCommandResult> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new Entities.ApplicationUser()
            {
                UserName = request.Name,
                Email = request.Email,
                PasswordHash = request.PasswordHash,
            };

            await _dbConext.Users.AddAsync(newUser);
            await _dbConext.SaveChangesAsync();

            return new AddUserCommandResult()
            {
                Id = newUser.Id
            };
        }
    }
}
