using QuizHubDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Requests
{
    public record CreateQuestionDto(int Order,
        string Text,
        int Points,
        int QuizId,
        QuestionType QuestionType,
        string? CorrectFillInAnswer,
        List<CreateAnswerOptionDto> AnswerOptions
        )
    {
    }
}
