using QuizHubDomain.Entities;
using QuizHubInfrastructure.Data;
using QuizHubInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubInfrastructure.Repositories
{
    public class QuestionRepository(QuizHubDbContext context) : IQuestionRepository
    {
        private readonly QuizHubDbContext _context = context;
        public Question CreateQuestion(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();
            return question;
        }

        public void DeleteQuestion(Question question)
        {
            question.IsActive = false;
            foreach (AnswerOption ao in question.AnswerOptions)
            { 
                ao.IsActive = false;
            }

            _context.Questions.Update(question);
            _context.SaveChanges();
        }

        public Question? GetQuestionWithAnswers(int questionId)
        {
            //aktivna pitanja i odgovori, kako bi ih posle izbrisala logicki
            //dovlaci sva polja kako mi ne bi izbrisao ostala polja nego samo promenio vrednost
            return _context.Questions.Where(q => q.Id == questionId && q.IsActive)
                .Select(q => new Question
                {
                    Id = q.Id,
                    Text = q.Text,
                    QuizId = q.QuizId,
                    Points = q.Points,
                    Order = q.Order,
                    QuestionType = q.QuestionType,
                    CorrectFillInAnswer = q.CorrectFillInAnswer,
                    IsActive = q.IsActive,
                    AnswerOptions = q.AnswerOptions
                    .Where(ao => ao.IsActive)
                    .Select(ao => new AnswerOption
                    {
                        Id = ao.Id,
                        Text = ao.Text,
                        IsCorrect = ao.IsCorrect,
                        QuestionId = ao.QuestionId,
                        IsActive = ao.IsActive
                    }
                    ).ToList()
                })
                .FirstOrDefault();

        }

        public void UpdateQuestion(Question question)
        {

            _context.Questions.Update(question);
            _context.SaveChanges();

        }
    }
}
