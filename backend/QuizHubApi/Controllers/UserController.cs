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

        [HttpPost("register")]
        public ActionResult<UserDto> CreateUser([FromForm] CreateUserDto createUserDto)
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

        [HttpPost("login")]
        public ActionResult<TokenResponseDto> Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                
                return Ok(_userService.Login(loginUserDto));
            }
            catch (Exception ex) when (ex is EntityAlreadyExists or InvalidPassword or EntityDoesNotExist)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUserById(int id)
        {
            try
            {
                UserDto? user = _userService.GetUserByIdDto(id);
                return Ok(user);
            }
            catch (Exception ex) when (ex is EntityDoesNotExist)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
