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
    public interface IQuestionService
    {
        QuestionDto? GetQuestion(int number);
        QuizDetailsDto? DeleteQuestion(int quizId, int questionId);
        QuizDetailsDto? UpdateQuestion(UpdateQuestionDto updateQuestionDto);
        QuizDetailsDto? CreateQuestionDto (CreateQuestionDto  createQuestionDto);
        Question? GetQuestionWithAnswers(int questionId);
    }
}
