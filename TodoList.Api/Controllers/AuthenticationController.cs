using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Services.Interfaces;
//using TodoList.DAL.Queries.GetUserToken;
//using TodoList.DAL.Queries.LogInUser;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthenticationController(
           IUserService userService)
        {
            _userService = userService;
        }
        //[HttpPost]
        //public async Task<AddUserResponse> AddUser(AddUserRequest request)
        //{
        //    return await _userService.AddUser(request);
        //}

        //[HttpPost]
        //public async Task<ActionResult<LogInUserQueryResult>> LogIn(LogInUser user)
        //{
        //    var result = await _userService.LogInUser(user);
        //    if (result.Token == "null")
        //    {
        //        return BadRequest("Wrong email adress.");
        //    }
        //    if (result.Token == "wrongpassword")
        //    {
        //        return BadRequest("Wrong password.");
        //    }
        //    return result;
        //}

        //[HttpPost()]
        //public async Task<ActionResult<GetUserTokenQueryResult>> GetToken()
        //{
        //    var result = _userService.GetUserToken();
        //    return Ok(result);
        //}
    }
}
