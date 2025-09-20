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
    public class QuizAttemptRepository(QuizHubDbContext context) : IQuizAttemptRepository
    {
        private readonly QuizHubDbContext _context = context;

        public void CreateQuizAttempt(QuizAttempt quizAttempt)
        {
            _context.QuizAttempts.Add(quizAttempt);
            _context.SaveChanges();
        }

        public QuizAttempt? GetQuizAttempt(int quizAttemptById)
        {
            return _context.QuizAttempts
                    .Where(qa => qa.Id == quizAttemptById)
                    .Select(qa => new QuizAttempt
                    {
                        Id = qa.Id,
                        StartedAt = qa.StartedAt,
                        FinishedAt = qa.FinishedAt,
                        Score = qa.Score,
                        UserId = qa.UserId,
                        QuizId = qa.QuizId,
                        AttemptAnswers = qa.AttemptAnswers.Select(aa => new AttemptAnswer
                        {
                            Id = aa.Id,
                            QuizAttemptId = aa.QuizAttemptId,
                            FillInAnswer = aa.FillInAnswer,
                            IsCorrect = aa.IsCorrect,
                            AwardedPoints = aa.AwardedPoints,
                            QuestionId = aa.QuestionId,
                            AttemptAnswerOptions = aa.AttemptAnswerOptions.Select(ao => new AttemptAnswerOption
                            {
                                Id = ao.Id,   
                                AnswerOptionId = ao.AnswerOptionId,
                                AttemptAnswerId = ao.AttemptAnswerId
                            }).ToList()
                        }).ToList()
                    })
                    .FirstOrDefault();
        }

        public QuizAttempt? GetQuizAttemptById(int quizAttemptId)
        {
            return _context.QuizAttempts
                .Where(qa => qa.Id == quizAttemptId)
                .FirstOrDefault();
        }

        public void UpdateQuizAttempt(QuizAttempt quizAttempt)
        {
            _context.QuizAttempts.Update(quizAttempt);
            _context.SaveChanges();
        }
    }
}