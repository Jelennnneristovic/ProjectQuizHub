using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Requests
{
    public record UpdateAnswerOptionDto(int AnswerOptionId,string Text, bool IsCorrect)
    {
    }
}
