using QuizHubDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Configuration
{
    public record UserContext(int Id, string UserName, string Email, Role Role)
    {
    }
}
