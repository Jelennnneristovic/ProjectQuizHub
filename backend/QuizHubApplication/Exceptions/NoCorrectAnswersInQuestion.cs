using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Exceptions
{
    public class NoCorrectAnswersInQuestion() : Exception("Invalid type no correct answers in question")
    {
    }
}
