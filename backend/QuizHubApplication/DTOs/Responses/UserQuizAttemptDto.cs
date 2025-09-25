using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Responses
{
    public record UserQuizAttemptDto(
        int QuizAttemptId,
        int QuizId,
        string Title,
        int TimeLimit,
        List<UserQuizAttemptQuestionDto> Questions
        )
    {
    }
}
