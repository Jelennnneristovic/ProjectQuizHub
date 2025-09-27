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
    public class ResultRepository(QuizHubDbContext context) : IResultRepository
    {
        readonly QuizHubDbContext _context = context;

        public void CreateResult(Result result)
        {
            _context.Results.Add(result);
            _context.SaveChanges();

        }

        public List<Result> GetLeaderboard(int quizId, string? period)
        {
            return [.. GetResults()
                .Where(r => r.QuizAttempt != null && r.QuizAttempt.QuizId == quizId
                && (period is null || 
                    (period.Equals("weekly") && r.CreatedAt >= DateTime.Now.AddDays(-7)) ||
                    (period.Equals("monthly") && r.CreatedAt >= DateTime.Now.AddMonths(-1))
                   )
                )
                .OrderByDescending(r => r.Score)
                .ThenBy(r => r.TimeTakenMin)];


        }

        public Result? GetResultDetailsById(int resultId)
        {
            return _context.Results
                .Where(r => r.Id == resultId)
                .Select(r => new Result
                {
                    Id = r.Id,
                    QuizAttemptId = r.QuizAttemptId,
                    QuizTitle = r.QuizTitle,
                    TotalQuestions = r.TotalQuestions,
                    CorrectAnswers = r.CorrectAnswers,
                    Score = r.Score,
                    Percentage = r.Percentage,
                    TimeTakenMin = r.TimeTakenMin,
                    CreatedAt = r.CreatedAt,
                    QuizAttempt = r.QuizAttempt != null ? new QuizAttempt()
                    {
                        Id = r.QuizAttempt.Id,
                        QuizId= r.QuizAttempt.QuizId,
                        UserId= r.QuizAttempt.UserId,
                        AttemptAnswers = r.QuizAttempt.AttemptAnswers
                                         .Select(aa => new AttemptAnswer
                                         {

                                             Question = aa.Question != null ? new Question
                                             {
                                                 Id= aa.Question.Id,

                                                 Text = aa.Question.Text,
                                                 Points = aa.Question.Points,
                                                 QuestionType=aa.Question.QuestionType,
                                                 CorrectFillInAnswer=aa.Question.CorrectFillInAnswer,

                                                 AnswerOptions=aa.Question.AnswerOptions
                                                 .Select(ao => new AnswerOption
                                                 { 
                                                    Id = ao.Id,
                                                    Text = ao.Text,
                                                    IsCorrect=ao.IsCorrect,
                                                 } )
                                                    .ToList()
                                             } : null,
                                             QuestionId=aa.QuestionId,
                                             IsCorrect = aa.IsCorrect,
                                             FillInAnswer = aa.FillInAnswer,
                                             AwardedPoints=aa.AwardedPoints,

                                             AttemptAnswerOptions = aa.AttemptAnswerOptions
                                                .Select(ao => new AttemptAnswerOption
                                                {
                                                    Id=ao.Id,
                                                    AnswerOptionId=ao.AnswerOptionId,

                                                    AnswerOption = ao.AnswerOption != null ? new AnswerOption
                                                    {
                                                        Id= ao.AnswerOption.Id,
                                                        Text = ao.AnswerOption.Text,
                                                        IsCorrect = ao.AnswerOption.IsCorrect

                                                    } : null

                                                }).ToList(),
                                         }).ToList(),
                    } : null
                })
                .FirstOrDefault();
        }

        public List<Result> GetResults()
        {
           return _context.Results
                .Select(r => new Result
                {
                    Id = r.Id,
                    QuizAttemptId = r.QuizAttemptId,
                    QuizTitle = r.QuizTitle,
                    TotalQuestions = r.TotalQuestions,
                    CorrectAnswers = r.CorrectAnswers,
                    Score = r.Score,
                    Percentage = r.Percentage,
                    TimeTakenMin = r.TimeTakenMin,
                    CreatedAt = r.CreatedAt,
                    QuizAttempt = r.QuizAttempt != null ? new QuizAttempt()
                    {
                        QuizId = r.QuizAttempt.QuizId,
                        UserId = r.QuizAttempt.UserId,
                        User = r.QuizAttempt.User != null ? new User()
                        {
                            UserName = r.QuizAttempt.User.UserName
                        } : null

                    } : null
                })
                .ToList();
        }

        public List<Result> GetResults(int UserId)
        {

             return GetResults()
                .Where(r => r.QuizAttempt != null && r.QuizAttempt.UserId == UserId)
                .ToList();
        }

        public List<Result> GetResultsByQuizIdAndUserIdOrderByCreatedAt(int quizId, int userId)
        {
            return _context.Results.Where(r=> r.QuizAttempt != null && r.QuizAttempt.QuizId == quizId && r.QuizAttempt.UserId == userId) 
                .OrderBy(r => r.CreatedAt)
                .Select(r=> new Result { 
                    Score= r.Score,
                    CreatedAt= r.CreatedAt,

                    QuizAttempt=r.QuizAttempt !=null ? new QuizAttempt()
                    { 
                        Id=r.QuizAttempt.Id,
                        QuizId=r.QuizAttempt.QuizId
                    } : null

                
                }).ToList();
        }
    }
}
