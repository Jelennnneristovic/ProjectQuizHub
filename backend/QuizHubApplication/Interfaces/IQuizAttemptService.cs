using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.DTOs.Responses;
using QuizHubDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Interfaces
{
    public interface IQuizAttemptService
    {
        QuizAttemptDto CreateQuizAttemptDto(CreateQuizAttemptDto createQuizAttemptDto);
        QuizAttempt? GetQuizAttemptById(int quizAttemptId);
        QuizResultDto FinishQuizAttempt(int quizAttemptId);
    }
}
