using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Requests
{
    public record CreateUserDto( string Username, string Email, string Password, string? AvatarUrl)
    {
    }
}
