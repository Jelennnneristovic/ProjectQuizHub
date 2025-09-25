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
        int QuizId,
        string StartedAt,
        string? FinisedAt,
        int? TimeTakenMin,
        int Score)
    {
    }
}
