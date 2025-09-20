using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.DTOs.Responses;
using QuizHubDomain.Entities;
using QuizHubDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Interfaces
{
    public interface IQuizService
    {
        List<QuizDto> GetQuizzes();
        // QuizDto? GetQuiz(string name);
        List<QuizDto> GetQuizzesSearch(DifficultyLevel? difficultyLevel, string? categoryName);
        List<QuizDto> GetQuizzesByKeyWord(string keyword);
        QuizDetailsDto? GetQuiz(int id);
        QuizDto CreateQuiz(CreateQuizDto createQuizDto);
        string DeleteQuiz(int id);
        QuizDto? UpdateQuiz(UpdateQuizDto updateQuizDto);

        Quiz? GetQuizById(int id);
    }
}
