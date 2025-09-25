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
        UserQuizAttemptDto CreateQuizAttempt(CreateQuizAttemptDto createQuizAttemptDto);

        QuizAttempt? GetQuizAttemptById(int quizAttemptId);
        QuizResultDto FinishQuizAttempt(int quizAttemptId);
        List<QuizAttemptDto> GetAllQuizAttempts();
        List<QuizAttemptDto> GetQuizAttemptsByUserId(int userId);
        List<QuizAttemptDto> GetQuizAttemptsFromUser();
    }
}
