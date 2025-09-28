using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Interfaces
{
    public interface IUserService
    {
        UserDto CreateUser(CreateUserDto createUserDto);
        TokenResponseDto Login(LoginUserDto loginUserDto);
        UserDto GetUserByIdDto(int id);
    }
}
