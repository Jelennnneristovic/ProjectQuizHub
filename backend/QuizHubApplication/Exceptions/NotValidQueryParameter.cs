using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Exceptions
{
    public class NotValidQueryParameter(string message) : Exception(message)
    {
    }
}
