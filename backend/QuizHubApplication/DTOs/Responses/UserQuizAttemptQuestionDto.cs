using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Responses
{
    public record UserQuizAttemptQuestionDto(int Id, string Text, int Points, string QuestionType, List<UserQuizAttemptOptionDto> OptionDtos)
    {
    }
}
