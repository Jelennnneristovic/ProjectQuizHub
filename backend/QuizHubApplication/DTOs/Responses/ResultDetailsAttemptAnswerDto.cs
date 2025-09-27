using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Responses
{
    public record ResultDetailsQuizAttemptAnswerDto(
        string QuestionText,
        bool IsCorrect,
        string? CorrectFillInAnswer,
        List<ResultDetailsAttemptAnswerOptionDto> AllAnswers,
        string? FillInAnswer,
        List<ResultDetailsAttemptAnswerOptionDto> UserAnswers,
        string QusetionType,
        int QuestionPoints,
        int AwardedPoints
        )
    {
    }
}
