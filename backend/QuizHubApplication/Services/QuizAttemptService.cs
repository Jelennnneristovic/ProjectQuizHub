using QuizHubApplication.Configuration;
using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.DTOs.Responses;
using QuizHubApplication.Exceptions;
using QuizHubApplication.Interfaces;
using QuizHubDomain.Entities;
using QuizHubInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Services
{

    //prvo kreiram prazan da bih dobavila Id koji mi treba za pitanja i odgovore.
    public class QuizAttemptService(IQuizAttemptRepository quizAttemptRepository, ITokenService tokenService, IQuizService quizService) : IQuizAttemptService
    {
        private readonly IQuizAttemptRepository _quizAttemptRepository = quizAttemptRepository;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IQuizService _quizService = quizService;

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

            return new(newQuizAttempt.Id);

        }

        public QuizResultDto FinishQuizAttempt(int quizAttemptId)
        {
            QuizAttempt? quizAttempt = _quizAttemptRepository.GetQuizAttemptById(quizAttemptId);
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
            quizAttempt.TimeTakenSeconds = (int)timeTaken.TotalSeconds;

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

            return new (questionCount,correctQuestionCount,(float)(correctQuestionCount/questionCount)*100f);
        }

        public QuizAttempt? GetQuizAttemptById(int quizAttemptId)
        {
            return _quizAttemptRepository.GetQuizAttemptById(quizAttemptId);
        }
    }
}
