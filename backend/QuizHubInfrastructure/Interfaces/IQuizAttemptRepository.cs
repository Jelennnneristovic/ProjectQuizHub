using QuizHubDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubInfrastructure.Interfaces
{
    public interface IQuizAttemptRepository
    {
        void CreateQuizAttempt(QuizAttempt quizAttempt);
        QuizAttempt? GetQuizAttemptById(int quizAttemptId);
        void UpdateQuizAttempt(QuizAttempt quizAttempt);
        QuizAttempt? GetQuizAttempt(int quizAttemptById);

        List<QuizAttempt> GetAllQuizAttempts();
        List<QuizAttempt> GetQuizAttemptsByUserId(int userId);
    }
}
