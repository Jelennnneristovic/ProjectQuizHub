using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.DTOs.Responses;
using QuizHubApplication.Exceptions;
using QuizHubApplication.Interfaces;
using QuizHubApplication.Services;

namespace QuizHubApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("/register")]
        public ActionResult<UserDto> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                UserDto? user = _userService.CreateUser(createUserDto);

                return Ok(user);
            }
            catch (EntityAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("/login")]
        public ActionResult<string> Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                
                return Ok(_userService.Login(loginUserDto));
            }
            catch (Exception ex) when (ex is EntityAlreadyExists or InvalidPassword)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpGet("/autenthication")]
        public ActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are autenthicated!");

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/admin")]
        public ActionResult AdminOnlyEndpoint()
        {
            return Ok("You are admin!");

        }


    }
}
