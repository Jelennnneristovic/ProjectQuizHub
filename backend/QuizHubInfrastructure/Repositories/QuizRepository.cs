using QuizHubDomain.Entities;
using QuizHubDomain.Enums;
using QuizHubInfrastructure.Data;
using QuizHubInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuizHubInfrastructure.Repositories
{
    public class QuizRepository(QuizHubDbContext context) : IQuizRepository
    {
        private readonly QuizHubDbContext _context = context;

        public Quiz CreateQuiz(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            _context.SaveChanges();
            return quiz;
        }

        public void DeleteQuiz(Quiz quiz)
        {

            quiz.IsActive = false;

            foreach (Question q in quiz.Questions)
            {
                q.IsActive = false;

                foreach (AnswerOption a in q.AnswerOptions)
                {
                    a.IsActive = false;
                }
            }

            _context.Quizzes.Update(quiz);
            _context.SaveChanges();
        }

        public Quiz? GetQuiz(int id)
        {


            return _context.Quizzes
                .Where(x => x.IsActive && x.Id == id)
                .Select(q => new Quiz
                {
                    Id = q.Id,
                    Title = q.Title,
                    Description = q.Description,
                    TimeLimit = q.TimeLimit,
                    DifficultyLevel = q.DifficultyLevel,
                    IsActive = q.IsActive,
                    Category = q.Category != null && q.Category.IsActive ? q.Category : null,
                    Questions = q.Questions
                    .Where(ques => ques.IsActive)
                    .Select(question => new Question
                    {
                        Id = question.Id,
                        Text = question.Text,
                        Points = question.Points,
                        QuestionType = question.QuestionType,
                        CorrectFillInAnswer = question.CorrectFillInAnswer,
                        IsActive = question.IsActive,
                        QuizId = question.QuizId,
                        AnswerOptions = question.AnswerOptions.Where(a => a.IsActive)
                        .Select(a => new AnswerOption
                        {
                            Id = a.Id,
                            Text = a.Text,
                            IsCorrect = a.IsCorrect,
                            IsActive = a.IsActive,
                            QuestionId = a.QuestionId

                        }).ToList(),
                    }).ToList()
                })
                .FirstOrDefault();
        }

        public List<Quiz> GetQuizzes()
        {
            return [.. _context.Quizzes.Where(x => x.IsActive)
                .Select(q => new Quiz
                {
                    Id = q.Id,
                    Title = q.Title,
                    Description = q.Description,
                    TimeLimit = q.TimeLimit,
                    DifficultyLevel = q.DifficultyLevel,
                    Category = q.Category != null && q.Category.IsActive ? q.Category : null,
                    Questions = q.Questions.Where(ques => ques.IsActive).Select(question => new Question { Id = question.Id}).ToList(),

                })];
        }

        public List<Quiz> GetQuizzesSearch(DifficultyLevel? difficultyLevel, string? categoryName)
        {
            List<Quiz> quizzes = GetQuizzes(); //svi aktivni kvizovi

            quizzes = [.. quizzes

              .Where(q =>
                   (difficultyLevel == null || q.DifficultyLevel == difficultyLevel) &&
                   (string.IsNullOrEmpty(categoryName) || (q.Category != null && q.Category.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase)))
                   
              )];


            return quizzes;
        }

        public List<Quiz> GetQuizzesWithAllDetailsForKeyWord()
        {
            return _context.Quizzes
                .Where(x => x.IsActive)
                .Select(q => new Quiz
                {
                    Id = q.Id,
                    Title = q.Title,
                    Description = q.Description,
                    TimeLimit = q.TimeLimit,
                    Category = q.Category != null && q.Category.IsActive ? q.Category : null,
                    Questions = q.Questions


                    .Where(ques => ques.IsActive)
                    .Select(question => new Question
                    {
                        Id = question.Id,
                        Text = question.Text,
                        AnswerOptions = question.AnswerOptions.Where(a => a.IsActive)
                        .Select(a => new AnswerOption
                        {
                            Id = a.Id,
                            Text = a.Text,

                        }).ToList(),
                    }).ToList()
                })
                .ToList();
        }

        public void UpdateQuiz(Quiz quiz)
        {

            _context.Quizzes.Update(quiz);
            _context.SaveChanges();

        }
    }
}
