using Microsoft.EntityFrameworkCore;
using QuizHubDomain.Entities;
using QuizHubDomain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubInfrastructure.Data
{
    public class QuizHubDbContext(DbContextOptions<QuizHubDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Quiz> Quizzes => Set<Quiz>();
        public DbSet<AnswerOption> AnswerOptions => Set<AnswerOption>();
        public DbSet<AttemptAnswer> AttemptAnswers => Set<AttemptAnswer>();

        public DbSet<Question> Questions => Set<Question>();
        public DbSet<QuizAttempt> QuizAttempts => Set<QuizAttempt>();
        public DbSet<AttemptAnswerOption> AttemptAnswerOptions => Set<AttemptAnswerOption>();
        public DbSet<Result> Results => Set<Result>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Primer: AttemptAnswer → QuizAttempt
            modelBuilder.Entity<AttemptAnswer>()
                .HasOne(a => a.QuizAttempt)
                .WithMany(q => q.AttemptAnswers)
                .HasForeignKey(a => a.QuizAttemptId)
                .OnDelete(DeleteBehavior.Restrict); // umesto Cascade

            // Primer: AttemptAnswerOption → AttemptAnswer
            modelBuilder.Entity<AttemptAnswerOption>()
                .HasOne(aa => aa.AttemptAnswer)
                .WithMany(a => a.AttemptAnswerOptions)
                .HasForeignKey(aa => aa.AttemptAnswerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Primer: AnswerOption → Question
            modelBuilder.Entity<AnswerOption>()
                .HasOne(o => o.Question)
                .WithMany(q => q.AnswerOptions)
                .HasForeignKey(o => o.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Result → QuizAttempt (1:1)
            modelBuilder.Entity<QuizAttempt>()
                .HasOne(qa => qa.Result)
                .WithOne(r => r.QuizAttempt)
                .HasForeignKey<Result>(r => r.QuizAttemptId)
                .OnDelete(DeleteBehavior.Cascade);

            //Users
            var admin = new User
            {
                Id = 1,
                UserName = "admin",
                Email = "admin@admin.com",
                PasswordHash = "AQAAAAIAAYagAAAAEFg+InxaoNsss+/H2jitDRm8G652TpCi7RMkdRoeNeVai+H3/7foRQ0XtTmgpkQ+WQ==",
                AvatarUrl = null,
                Role = Role.Admin,
            };

            var user1 = new User
            {
                Id = 2,
                UserName = "marko",
                Email = "marko@cake.com",
                PasswordHash = "AQAAAAIAAYagAAAAEFg+InxaoNsss+/H2jitDRm8G652TpCi7RMkdRoeNeVai+H3/7foRQ0XtTmgpkQ+WQ==",
                AvatarUrl = null,
                Role = Role.User,
            };
            var user2 = new User
            {
                Id = 3,
                UserName = "ana",
                Email = "ana@cake.com",
                PasswordHash = "AQAAAAIAAYagAAAAEFg+InxaoNsss+/H2jitDRm8G652TpCi7RMkdRoeNeVai+H3/7foRQ0XtTmgpkQ+WQ==",
                AvatarUrl = null,
                Role = Role.User,
            };
            var user3 = new User
            {
                Id = 4,
                UserName = "stefan",
                Email = "stefan@cake.com",
                PasswordHash = "AQAAAAIAAYagAAAAEFg+InxaoNsss+/H2jitDRm8G652TpCi7RMkdRoeNeVai+H3/7foRQ0XtTmgpkQ+WQ==",
                AvatarUrl = null,
                Role = Role.User,
            };
            modelBuilder.Entity<User>().HasData(
                admin,
                user1,
                user2,
                user3
            );

            //Categories
            var category1 = new Category
            {
                Id = 1,
                Name = "Programiranje",
                Description = "Generalno sve o programiranju",
                IsActive = true
            };
            var category2 = new Category
            {
                Id = 2,
                Name = "Arhitektura",
                Description = "Rimska i anticka arhitektura",
                IsActive = true
            };
            var category3 = new Category
            {
                Id = 3,
                Name = "Istorija",
                Description = "Opste znanje",
                IsActive = true
            };
            var category4 = new Category
            {
                Id = 4,
                Name = "Muzika",
                Description = "Klasicna muzika",
                IsActive = true
            };

            modelBuilder.Entity<Category>().HasData(
                category1,
                category2,
                category3,
                category4
            );

            //Quizzes
            var quiz1 = new Quiz
            {
                Id = 1,
                Title = "Kviz programiranja",
                Description = null,
                TimeLimit = 3,
                DifficultyLevel = DifficultyLevel.Easy,
                CategoryId = category1.Id,
                IsActive = true,
            };
            var quiz2 = new Quiz
            {
                Id = 2,
                Title = "Kviz o arhitekturi Novog Sada",
                Description = "Istorija arhitektura grada Novog Sada",
                TimeLimit = 3,
                DifficultyLevel = DifficultyLevel.Medium,
                CategoryId = category2.Id,
                IsActive = true,
            };
           
            modelBuilder.Entity<Quiz>().HasData(
                quiz1,
                quiz2
            );

            //Questions
            var question1 = new Question
            {
                Id = 1,
                Text = "Koji od navedenih programa podrzava klase",
                QuizId = quiz1.Id,
                Points = 3,
                QuestionType = QuestionType.MultipleChoice,
                CorrectFillInAnswer = null,
                IsActive = true,
            };
            var question2 = new Question
            {
                Id = 2,
                Text = "Metoda Console.WriteLine() vraca vrednost string?",
                QuizId = quiz1.Id,
                Points = 2,
                QuestionType = QuestionType.TrueFalse,
                CorrectFillInAnswer = null,
                IsActive = true,
            };
          
            var question3 = new Question
            {
                Id = 3,
                Text = "Ciji spomenik se nalazi na trgu u centru grada?",
                QuizId = quiz2.Id,
                Points = 2,
                QuestionType = QuestionType.FillIn,
                CorrectFillInAnswer = "Svetozar Miletic",
                IsActive = true,
            };
            modelBuilder.Entity<Question>().HasData(
                question1,
                question2,
                question3
                
            );

            // AnswerOptions
            var answerOption1 = new AnswerOption
            {
                Id = 1,
                Text = "Java",
                QuestionId = question1.Id,
                IsCorrect = true,
                IsActive = true,
            };
            var answerOption2 = new AnswerOption
            {
                Id = 2,
                Text = "C#",
                QuestionId = question1.Id,
                IsCorrect = true,
                IsActive = true
            };
            var answerOption3 = new AnswerOption
            {
                Id = 3,
                Text = "C++",
                QuestionId = question1.Id,
                IsCorrect = true,
                IsActive = true
            };
            var answerOption4 = new AnswerOption
            {
                Id = 4,
                Text = "True",
                QuestionId = question2.Id,
                IsCorrect = true,
                IsActive = true
            };
            var answerOption5 = new AnswerOption
            {
                Id = 5,
                Text = "False",
                QuestionId = question2.Id,
                IsCorrect = false,
                IsActive = true
            };

            modelBuilder.Entity<AnswerOption>().HasData(
                answerOption1,
                answerOption2,
                answerOption3,
                answerOption4,
                answerOption5
            );

            //QuizAttempts
            var quizAttempt1 = new QuizAttempt
            {
                Id = 1,
                QuizId = quiz1.Id,
                UserId = user1.Id,
                StartedAt = DateTime.Parse("2025-09-13 15:30:00"),
                FinishedAt = DateTime.Parse("2025-09-13 15:32:00"),
                TimeTakenMin = 2,
                Score = 5,
                CurrentQuizScore = 5,
                CurrentQuestionCount = 2
            };

            var quizAttempt2 = new QuizAttempt
            {
                Id = 2,
                QuizId = quiz2.Id,
                UserId = user2.Id,
                StartedAt = DateTime.Parse("2025-09-28 18:30:00"),
                FinishedAt = DateTime.Parse("2025-09-28 18:32:00"),
                TimeTakenMin = 2,
                Score = 2,
                CurrentQuizScore = 2,
                CurrentQuestionCount = 1
            };

            modelBuilder.Entity<QuizAttempt>().HasData(
                quizAttempt1,
                quizAttempt2
            );

            //AttemptAnswers
            var attemptAnswer1 = new AttemptAnswer
            {
                Id = 1,
                QuizAttemptId = quizAttempt1.Id,
                QuestionId = question1.Id,
                FillInAnswer = null,
                IsCorrect = true,
                AwardedPoints = question1.Points,
            };
            var attemptAnswer2 = new AttemptAnswer
            {
                Id = 2,
                QuizAttemptId = quizAttempt1.Id,
                QuestionId = question2.Id,
                FillInAnswer = null,
                IsCorrect = true,
                AwardedPoints = question2.Points,
            };

            var attemptAnswer3 = new AttemptAnswer
            {
                Id = 3,
                QuizAttemptId = quizAttempt2.Id,
                QuestionId = question3.Id,
                FillInAnswer = "Svetozara Miletica",
                IsCorrect = true,
                AwardedPoints = question3.Points,
            };


            modelBuilder.Entity<AttemptAnswer>().HasData(
                attemptAnswer1,
                attemptAnswer2,
                attemptAnswer3
            );

            //AttemptAnswerOptions

            var attemptAnswerOption1 = new AttemptAnswerOption
            {
                Id = 1,
                AttemptAnswerId = attemptAnswer1.Id,
                AnswerOptionId = answerOption1.Id,
            };
            var attemptAnswerOption2 = new AttemptAnswerOption
            {
                Id = 2,
                AttemptAnswerId = attemptAnswer1.Id,
                AnswerOptionId = answerOption2.Id,
            };
            var attemptAnswerOption3 = new AttemptAnswerOption
            {
                Id = 3,
                AttemptAnswerId = attemptAnswer1.Id,
                AnswerOptionId = answerOption3.Id,
            };
            var attemptAnswerOption4 = new AttemptAnswerOption
            {
                Id = 4,
                AttemptAnswerId = attemptAnswer2.Id,
                AnswerOptionId = answerOption4.Id,
            };

            modelBuilder.Entity<AttemptAnswerOption>().HasData(
                attemptAnswerOption1,
                attemptAnswerOption2,
                attemptAnswerOption3,
                attemptAnswerOption4
            );

            var result1 = new Result
            {
                Id = 1,
                QuizAttemptId = quizAttempt1.Id,
                QuizTitle = quiz1.Title,
                TotalQuestions = 2,
                CorrectAnswers = 2,
                Score = 5,
                MaximumScore = 5,
                Percentage = 100,
                TimeTakenMin = 2,
                CreatedAt = (DateTime)quizAttempt1.FinishedAt
            };

            var result2 = new Result
            {
                Id = 2,
                QuizAttemptId = quizAttempt2.Id,
                QuizTitle = quiz2.Title,
                TotalQuestions = 1,
                CorrectAnswers = 1,
                Score = 2,
                MaximumScore = 2,
                Percentage = 100,
                TimeTakenMin = 2,
                CreatedAt = (DateTime)quizAttempt2.FinishedAt
            };

            modelBuilder.Entity<Result>().HasData(
                result1,
                result2
            );
        }
    }
}
