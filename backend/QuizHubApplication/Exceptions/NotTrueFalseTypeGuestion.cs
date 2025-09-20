using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Exceptions
{
    public class NotTrueFalseTypeGuestion(): Exception("The question type must be True/False type to set the correct answer.")
    {
    }
}
