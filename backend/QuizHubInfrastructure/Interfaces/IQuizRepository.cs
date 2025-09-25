using QuizHubDomain.Entities;
using QuizHubDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubInfrastructure.Interfaces
{
    public interface IQuizRepository
    {
        List<Quiz> GetQuizzes();
        List<Quiz> GetQuizzesSearch(DifficultyLevel? difficultyLevel, string? categoryName);
       // List<Quiz> GetQuizzesSearchKeyWord(string keyword);
        List<Quiz> GetQuizzesWithAllDetailsForKeyWord();

        Quiz? GetQuiz(int Id);

        Quiz CreateQuiz(Quiz quiz);

        void DeleteQuiz(Quiz quiz);

        void UpdateQuiz(Quiz quiz);
    }
}
