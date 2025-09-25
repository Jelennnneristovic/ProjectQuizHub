using Microsoft.EntityFrameworkCore;
using QuizHubApplication.Configuration;
using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.DTOs.Responses;
using QuizHubApplication.Exceptions;
using QuizHubApplication.Interfaces;
using QuizHubDomain.Entities;
using QuizHubDomain.Enums;
using QuizHubInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Services
{

    //prvo kreiram prazan da bih dobavila Id koji mi treba za pitanja i odgovore.
    public class QuizAttemptService(IQuizAttemptRepository quizAttemptRepository, ITokenService tokenService, IQuizService quizService, IResultService resultService) : IQuizAttemptService
    {
        private readonly IQuizAttemptRepository _quizAttemptRepository = quizAttemptRepository;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IQuizService _quizService = quizService;
        private readonly IResultService _resultService = resultService;

        public QuizAttemptDto CreateQuizAttemptDto(CreateQuizAttemptDto createQuizAttemptDto)
        {
            Quiz? quiz = _quizService.GetQuizById(createQuizAttemptDto.QuizId);
            if (quiz is null)
            {
                throw new EntityDoesNotExist(string.Format("Quiz with id {0} does not exist.", createQuizAttemptDto.QuizId));
            }

            //izvlacim informacije o useru iz tokena
            UserContext context = _tokenService.GetUserContext();
            QuizAttempt newQuizAttempt = new (createQuizAttemptDto.QuizId, context.Id);
            _quizAttemptRepository.CreateQuizAttempt(newQuizAttempt);

            return new(newQuizAttempt.Id, newQuizAttempt.UserId, newQuizAttempt.QuizId, newQuizAttempt.StartedAt.ToString("yyyy-MM-dd HH:mm:ss"),null,newQuizAttempt.TimeTakenMin,newQuizAttempt.Score);

        }

        public QuizResultDto FinishQuizAttempt(int quizAttemptId)
        {
            QuizAttempt? quizAttempt = _quizAttemptRepository.GetQuizAttempt(quizAttemptId);
            if (quizAttempt is null)
            {
                throw new EntityDoesNotExist(string.Format("Quiz attempt with id {0} does not exist.", quizAttemptId));
            }
       
            if (quizAttempt.FinishedAt is not null)
            { 
                throw new QuizAttemptAlreadyFinished(string.Format("Quiz attempt with id {0} is already finished.", quizAttemptId));

            }
            QuizDetailsDto? quizDetails = _quizService.GetQuiz(quizAttempt.QuizId);
            if (quizDetails is null)
            {
                throw new EntityDoesNotExist(string.Format("Quiz with id {0} does not exist.", quizDetails.Id));
            }

            //kviz je zavrsen
            quizAttempt.FinishedAt = DateTime.Now;
            //finishedat - started
            TimeSpan timeTaken = (TimeSpan)(quizAttempt.FinishedAt - quizAttempt.StartedAt);
            quizAttempt.TimeTakenMin = (int)timeTaken.TotalMinutes;

            //racunanjje tacnih odgovora
            int questionCount = quizDetails.Questions.Count;
            int correctQuestionCount = 0;
            int score = 0;
            foreach (AttemptAnswer aa in quizAttempt.AttemptAnswers)
            {
                if (aa.IsCorrect)
                { 
                    correctQuestionCount++;
                    score+=aa.AwardedPoints;

                }
            }
            quizAttempt.Score = score;
            _quizAttemptRepository.UpdateQuizAttempt(quizAttempt);

            double percentage = (double)correctQuestionCount / questionCount * 100.0;
            Result newResult = new(
                quizAttempt.Id,
                quizDetails.Title,
                questionCount,
                correctQuestionCount,
                score,
                percentage,
                (int)timeTaken.TotalMinutes
            );

            _resultService.CreateResult(newResult);
            return new (questionCount,correctQuestionCount, percentage);
        }

        public List<QuizAttemptDto> GetAllQuizAttempts()
        {
            List<QuizAttempt> attempts = _quizAttemptRepository.GetAllQuizAttempts();
            List<QuizAttemptDto> result = [];
            foreach (QuizAttempt attempt in attempts)
            {
              QuizAttemptDto dto = new(
                    attempt.Id,
                    attempt.UserId,
                    attempt.QuizId,
                    attempt.StartedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    attempt.FinishedAt?.ToString("yyyy-MM-dd HH:mm:ss"),
                    attempt.TimeTakenMin,
                    attempt.Score
                );
                result.Add(dto);

            }
            return result;
        }

        public QuizAttempt? GetQuizAttemptById(int quizAttemptId)
        {
            return _quizAttemptRepository.GetQuizAttemptById(quizAttemptId);
        }

        public List<QuizAttemptDto> GetQuizAttemptsByUserId(int userId)
        {   /*
             try 
             {                 
                    UserContext context = _tokenService.GetUserContext();
                    if (context.Role != Role.Admin && context.Id != userId)
                    {
                        throw new UnauthorizedAccessException("You are not authorized to view quiz attempts of other users.");
                    }
             }
                catch (Exception)
                 {
                     throw new UnauthorizedAccessException("You are not authorized to view quiz attempts of other users.");
                 }
            */
            // Provera da li korisnik postoji

            QuizAttempt? userCheck = _quizAttemptRepository.GetQuizAttemptById(userId);
            if (userCheck is null)
            {
                throw new EntityDoesNotExist(string.Format("User with id {0} does not exist.", userId));
            }

            List<QuizAttempt> attempts = _quizAttemptRepository.GetQuizAttemptsByUserId(userId);
            
            if (attempts is null)
            {
                throw new UserDoesntHaveQuizAttempts();
            }

           List<QuizAttemptDto> result = [];
            foreach (QuizAttempt attempt in attempts)
            {
              QuizAttemptDto dto = new(
                    attempt.Id,
                    attempt.UserId,
                    attempt.QuizId,
                    attempt.StartedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    attempt.FinishedAt?.ToString("yyyy-MM-dd HH:mm:ss"),
                    attempt.TimeTakenMin,
                    attempt.Score
                );
                result.Add(dto);

            }
            return result;
        }

        public List<QuizAttemptDto> GetQuizAttemptsFromUser()
        {

            //izvlacim informacije o useru iz tokena
            UserContext context = _tokenService.GetUserContext();


            List<QuizAttempt> attempts = _quizAttemptRepository.GetQuizAttemptsByUserId(context.Id); //id od Usera

            if (attempts is null || attempts.Count==0)
            {
                throw new UserDoesntHaveQuizAttempts();
            }

            List<QuizAttemptDto> result = [];
            foreach (QuizAttempt attempt in attempts)
            {
                QuizAttemptDto dto = new(
                      attempt.Id,
                      attempt.UserId,
                      attempt.QuizId,
                      attempt.StartedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                      attempt.FinishedAt?.ToString("yyyy-MM-dd HH:mm:ss"),
                      attempt.TimeTakenMin,
                      attempt.Score
                  );
                result.Add(dto);

            }
            return result;
        }
    }
}
