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
                Description = "Objektno programiranje",
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
                TimeLimit = 60,
                DifficultyLevel = DifficultyLevel.Easy,
                CategoryId = category1.Id,
                IsActive = true,
            };
            var quiz2 = new Quiz
            {
                Id = 2,
                Title = "Kviz o arhitekturi Novog Sada",
                Description = null,
                TimeLimit = 60,
                DifficultyLevel = DifficultyLevel.Medium,
                CategoryId = category2.Id,
                IsActive = true,
            };
            var quiz3 = new Quiz
            {
                Id = 3,
                Title = "Kviz opsteg znanja istoriji",
                Description = null,
                TimeLimit = 60,
                DifficultyLevel = DifficultyLevel.Easy,
                CategoryId = category3.Id,
                IsActive = true,
            };
            var quiz4 = new Quiz
            {
                Id = 4,
                Title = "Kviz o muzici",
                Description = null,
                TimeLimit = 60,
                DifficultyLevel = DifficultyLevel.Hard,
                CategoryId = category4.Id,
                IsActive = true,
            };
            modelBuilder.Entity<Quiz>().HasData(
                quiz1,
                quiz2,
                quiz3,
                quiz4
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
                Order = 1,
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
                Order = 2,
                IsActive = true,
            };
            var question3 = new Question
            {
                Id = 3,
                Text = "Zid od ___ je simbol Hladnog rata u Nemackoj?",
                QuizId = quiz3.Id,
                Points = 2,
                QuestionType = QuestionType.FillIn,
                CorrectFillInAnswer = null,
                Order = 1,
                IsActive = true,
            };
            var question4 = new Question
            {
                Id = 4,
                Text = "Kojoj grupi instrumenata pripada bubanj?",
                QuizId = quiz4.Id,
                Points = 2,
                QuestionType = QuestionType.SingleChoice,
                CorrectFillInAnswer = null,
                Order = 1,
                IsActive = true,
            };
            modelBuilder.Entity<Question>().HasData(
                question1,
                question2,
                question3,
                question4
                
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

            var answerOption6 = new AnswerOption
            {
                Id = 6,
                Text = "berlinskog zida",
                QuestionId = question3.Id,
                IsCorrect = true,
                IsActive = true
            };

            var answerOption7 = new AnswerOption
            {
                Id = 7,
                Text = "gudacki",
                QuestionId = question4.Id,
                IsCorrect = false,
                IsActive = true
            };
            var answerOption8 = new AnswerOption
            {
                Id = 8,
                Text = "udaracki",
                QuestionId = question4.Id,
                IsCorrect = true,
                IsActive = true
            };
            var answerOption9 = new AnswerOption
            {
                Id = 9,
                Text = "duvacki",
                QuestionId = question4.Id,
                IsCorrect = false,
                IsActive = true
            };
            modelBuilder.Entity<AnswerOption>().HasData(
                answerOption1,
                answerOption2,
                answerOption3,
                answerOption4,
                answerOption5,
                answerOption6,
                answerOption7,
                answerOption8,
                answerOption9
            );

            //QuizAttempts
            var quizAttempt1 = new QuizAttempt
            {
                Id = 1,
                QuizId = quiz1.Id,
                UserId = user1.Id,
                StartedAt = DateTime.Parse("2025-08-13 15:30:00"),
                FinishedAt = DateTime.Parse("2025-08-13 16:30:00"),
                TimeTakenMin = 60,
                Score = 1,
            };
            var quizAttempt2 = new QuizAttempt
            {
                Id = 2,
                QuizId = quiz1.Id,
                UserId = user1.Id,
                StartedAt = DateTime.Parse("2025-08-14 18:30:00"),
                FinishedAt = DateTime.Parse("2025-08-14 19:30:00"),
                TimeTakenMin = 60,
                Score = 3,
            };
            var quizAttempt3 = new QuizAttempt
            {
                Id = 3,
                QuizId = quiz2.Id,
                UserId = user1.Id,
                StartedAt = DateTime.Parse("2025-09-15 15:30:00"),
                FinishedAt = DateTime.Parse("2025-09-15 16:30:00"),
                TimeTakenMin = 60,
                Score = 5,
            };
            var quizAttempt4 = new QuizAttempt
            {
                Id = 4,
                QuizId = quiz3.Id,
                UserId = user2.Id,
                StartedAt = DateTime.Parse("2025-09-16 15:30:00"),
                FinishedAt = DateTime.Parse("2025-09-16 16:30:00"),
                TimeTakenMin = 60,
                Score = 1,
            };
            var quizAttempt5 = new QuizAttempt
            {
                Id = 5,
                QuizId = quiz4.Id,
                UserId = user2.Id,
                StartedAt = DateTime.Parse("2025-09-17 15:00:00"),
                FinishedAt = DateTime.Parse("2025-09-17 16:00:00"),
                TimeTakenMin = 60,
                Score = 1,
            };
            var quizAttempt6 = new QuizAttempt
            {
                Id = 6,
                QuizId = quiz3.Id,
                UserId = user2.Id,
                StartedAt = DateTime.Parse("2025-09-18 15:00:00"),
                FinishedAt = DateTime.Parse("2025-09-18 15:20:00"),
                TimeTakenMin = 20,
                Score = 2,
            };

            var quizAttempt7 = new QuizAttempt
            {
                Id = 7,
                QuizId = quiz3.Id,
                UserId = user1.Id,
                StartedAt = DateTime.Parse("2025-09-18 16:00:00"),
                FinishedAt = DateTime.Parse("2025-09-18 16:30:00"),
                TimeTakenMin = 30,
                Score = 1,
            };

            var quizAttempt8 = new QuizAttempt
            {
                Id = 8,
                QuizId = quiz4.Id,
                UserId = user1.Id,
                StartedAt = DateTime.Parse("2025-09-19 14:00:00"),
                FinishedAt = DateTime.Parse("2025-09-19 15:00:00"),
                TimeTakenMin = 60,
                Score = 2,
            };

            var quizAttempt9 = new QuizAttempt
            {
                Id = 9,
                QuizId = quiz2.Id,
                UserId = user2.Id,
                StartedAt = DateTime.Parse("2025-09-20 18:00:00"),
                FinishedAt = DateTime.Parse("2025-09-20 18:40:00"),
                TimeTakenMin = 40,
                Score = 4,
            };

            var quizAttempt10 = new QuizAttempt
            {
                Id = 10,
                QuizId = quiz1.Id,
                UserId = user2.Id,
                StartedAt = DateTime.Parse("2025-09-21 12:00:00"),
                FinishedAt = DateTime.Parse("2025-09-21 12:25:00"),
                TimeTakenMin = 25,
                Score = 3,
            };
            var quizAttempt11 = new QuizAttempt
            {
                Id = 11,
                QuizId = quiz1.Id, // Programiranje
                UserId = user3.Id,
                StartedAt = DateTime.Parse("2025-09-22 10:00:00"),
                FinishedAt = DateTime.Parse("2025-09-22 10:40:00"),
                TimeTakenMin = 40,
                Score = 4,
            };

            var quizAttempt12 = new QuizAttempt
            {
                Id = 12,
                QuizId = quiz2.Id, // Arhitektura
                UserId = user3.Id,
                StartedAt = DateTime.Parse("2025-09-22 11:00:00"),
                FinishedAt = DateTime.Parse("2025-09-22 11:50:00"),
                TimeTakenMin = 50,
                Score = 2,
            };

            var quizAttempt13 = new QuizAttempt
            {
                Id = 13,
                QuizId = quiz4.Id, // Muzika
                UserId = user3.Id,
                StartedAt = DateTime.Parse("2025-09-23 09:00:00"),
                FinishedAt = DateTime.Parse("2025-09-23 09:20:00"),
                TimeTakenMin = 20,
                Score = 3,
            };
            modelBuilder.Entity<QuizAttempt>().HasData(
                quizAttempt1,
                quizAttempt2,
                quizAttempt3,
                quizAttempt4,
                quizAttempt5,
                quizAttempt6,
                quizAttempt7,
                quizAttempt8,
                quizAttempt9,
                quizAttempt10,
                quizAttempt11,
                quizAttempt12,
                quizAttempt13

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


            modelBuilder.Entity<AttemptAnswer>().HasData(
                attemptAnswer1,
                attemptAnswer2
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
                AnswerOptionId = answerOption1.Id,
            };
            var attemptAnswerOption3 = new AttemptAnswerOption
            {
                Id = 3,
                AttemptAnswerId = attemptAnswer1.Id,
                AnswerOptionId = answerOption1.Id,
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
                QuizAttemptId = 1,
                QuizTitle = quiz1.Title,
                TotalQuestions = 2,
                CorrectAnswers = 2,
                Score = 5,
                Percentage = 100,
                TimeTakenMin = 50,
                CreatedAt = DateTime.Parse("2025-08-13 16:30:00")
            };
            var result2 = new Result
            {
                Id = 2,
                QuizAttemptId = 2,   
                QuizTitle= quiz1.Title,
                TotalQuestions = 2,  
                CorrectAnswers = 1,  
                Score = 3,           
                Percentage = 50,
                TimeTakenMin = 20, //20 minuta
                CreatedAt = DateTime.Parse("2025-08-14 17:00:00")
            };

            var result3 = new Result
            {
                Id = 3,
                QuizAttemptId = 3,
                QuizTitle = quiz2.Title,
                TotalQuestions = 2,
                CorrectAnswers = 0,  
                Score = 0,
                Percentage = 0,
                TimeTakenMin = 10, // 10 minuta
                CreatedAt = DateTime.Parse("2025-09-15 17:30:00")
            };
            var result4 = new Result
            {
                Id = 4,
                QuizAttemptId = 6,
                QuizTitle = quiz3.Title,
                TotalQuestions = 1,
                CorrectAnswers = 1,
                Score = 2,
                Percentage = 100,
                TimeTakenMin = 20,
                CreatedAt = DateTime.Parse("2025-09-18 15:20:00")
            };

            var result5 = new Result
            {
                Id = 5,
                QuizAttemptId = 7,
                QuizTitle = quiz3.Title,
                TotalQuestions = 1,
                CorrectAnswers = 1,
                Score = 1,
                Percentage = 100,
                TimeTakenMin = 30,
                CreatedAt = DateTime.Parse("2025-09-18 16:30:00")
            };

            var result6 = new Result
            {
                Id = 6,
                QuizAttemptId = 8,
                QuizTitle = quiz4.Title,
                TotalQuestions = 1,
                CorrectAnswers = 1,
                Score = 2,
                Percentage = 100,
                TimeTakenMin = 60,
                CreatedAt = DateTime.Parse("2025-09-19 15:00:00")
            };

            var result7 = new Result
            {
                Id = 7,
                QuizAttemptId = 9,
                QuizTitle = quiz2.Title,
                TotalQuestions = 2,
                CorrectAnswers = 2,
                Score = 4,
                Percentage = 100,
                TimeTakenMin = 40,
                CreatedAt = DateTime.Parse("2025-09-20 18:40:00")
            };

            var result8 = new Result
            {
                Id = 8,
                QuizAttemptId = 10,
                QuizTitle = quiz1.Title,
                TotalQuestions = 2,
                CorrectAnswers = 1,
                Score = 3,
                Percentage = 50,
                TimeTakenMin = 25,
                CreatedAt = DateTime.Parse("2025-09-21 12:25:00")
            };
            var result9 = new Result
            {
                Id = 9,
                QuizAttemptId = 11,
                QuizTitle = quiz1.Title,
                TotalQuestions = 3,
                CorrectAnswers = 2,
                Score = 4,
                Percentage = 66,
                TimeTakenMin = 40,
                CreatedAt = DateTime.Parse("2025-09-22 10:40:00")
            };

            var result10 = new Result
            {
                Id = 10,
                QuizAttemptId = 12,
                QuizTitle = quiz2.Title,
                TotalQuestions = 3,
                CorrectAnswers = 1,
                Score = 2,
                Percentage = 33,
                TimeTakenMin = 50,
                CreatedAt = DateTime.Parse("2025-09-22 11:50:00")
            };

            var result11 = new Result
            {
                Id = 11,
                QuizAttemptId = 13,
                QuizTitle = quiz4.Title,
                TotalQuestions = 2,
                CorrectAnswers = 2,
                Score = 3,
                Percentage = 100,
                TimeTakenMin = 20,
                CreatedAt = DateTime.Parse("2025-09-23 09:20:00")
            };


            modelBuilder.Entity<Result>().HasData(
                result1,
                result2,
                result3,
                result4,
                result5,
                result6,
                result7,
                result8,
                result9,
                result10,
                result11


            );
        }
    }
}
