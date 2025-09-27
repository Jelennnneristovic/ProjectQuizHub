using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Responses
{
    public record QuestionDto(int Id, string Text, int Points, string QuestionType, string? CorrectFillInAnswer,
        List<AnswerOptionDto> AnswerOptions)
    {
    }
}
