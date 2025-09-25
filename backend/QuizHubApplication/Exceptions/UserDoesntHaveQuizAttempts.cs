using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Exceptions
{
    public class UserDoesntHaveQuizAttempts(): Exception("User doesn't have quiz attempts")
    {
    }
}
