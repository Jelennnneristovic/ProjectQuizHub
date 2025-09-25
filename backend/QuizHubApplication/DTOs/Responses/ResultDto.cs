using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Responses
{
    public record ResultDto(
     int Id,
     int QuizAttemptId,
     string QuizTitle,
     int TotalQuestions,
     int CorrectAnswers,
     int Score,
     double Percentage,
     int TimeTakenMin,
     string CreatedAt
    )
    {}
}
