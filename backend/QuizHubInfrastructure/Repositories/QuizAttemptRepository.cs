using Microsoft.EntityFrameworkCore;
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

        public List<QuizAttempt> GetAllQuizAttempts()
        {
            return _context.QuizAttempts

            .Select(qa => new QuizAttempt
            {
                Id = qa.Id,
                UserId = qa.UserId,
                User = qa.User != null ? new User
                {
                    Id = qa.UserId,
                    UserName = qa.User.UserName,
                    Email = qa.User.Email,
                    AvatarUrl = qa.User.AvatarUrl,
                    Role = qa.User.Role
                } : null,
                QuizId = qa.QuizId,
                Quiz = qa.Quiz != null ? new Quiz
                {
                    Id = qa.QuizId,
                    Title = qa.Quiz.Title,
                    Description = qa.Quiz.Description,
                    IsActive = qa.Quiz.IsActive,
                    TimeLimit = qa.Quiz.TimeLimit
                } : null,
                StartedAt = qa.StartedAt,
                FinishedAt = qa.FinishedAt,
                TimeTakenMin = qa.TimeTakenMin,
                Score = qa.Score,
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
            .ToList();

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
                                AttemptAnswerId = ao.AttemptAnswerId,
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

        public List<QuizAttempt> GetQuizAttemptsByUserId(int userId)
        {
            List<QuizAttempt> attempts = GetAllQuizAttempts()
                                         .Where(qa => qa.UserId == userId)
                                         .ToList();

            return attempts;
        }

        public void UpdateQuizAttempt(QuizAttempt quizAttempt)
        {
            _context.QuizAttempts.Update(quizAttempt);
            _context.SaveChanges();
        }
    }
}