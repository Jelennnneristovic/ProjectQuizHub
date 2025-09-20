using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Responses
{
    public record UserDto(string Username, string Email, string? AvatarUrl)
    {
    }
}
