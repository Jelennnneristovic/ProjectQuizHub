using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Responses
{
    public record QuizAttemptDto(
        int QuizAttemptId,
        int UserId,
        string Username,
        int QuizId,
        string QuizTitle,
        string StartedAt,
        string? FinisedAt,
        int TimeLimit,
        int? TimeTakenMin,
        int Score
        )
    {
    }
}
