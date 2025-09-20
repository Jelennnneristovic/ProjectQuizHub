using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Requests
{
    public record LoginUserDto(string UserKey, string Password)
    {
        //UserKey sifra ili email
    }
}
