using QuizHubApplication.Configuration;
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
    public class ResultService(IResultRepository resultRepository, ITokenService tokenService) : IResultService
    {
        private readonly IResultRepository _resultRepository = resultRepository;
        private readonly ITokenService _tokenService = tokenService;
        public List<ResultDto> GetResults()
        {

            List<Result> results = _resultRepository.GetResults();
            return CreateListResultDto(results);

        }

        public List<ResultDto> GetResultsByUser()
        {

            UserContext context = _tokenService.GetUserContext();
            List<Result> results = _resultRepository.GetResults(context.Id);
            return CreateListResultDto(results);

        }

        private static List <ResultDto> CreateListResultDto(List<Result> results)
        {
            List<ResultDto> resultDto = [];
            foreach (Result r in results)
            { 
                resultDto.Add(new ResultDto
                (   r.Id,
                    r.QuizAttemptId,
                    r.QuizTitle,
                    r.TotalQuestions,
                    r.CorrectAnswers,
                    r.Score,
                    r.Percentage,
                    r.TimeTakenMin,
                    r.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                ));

            }

            return resultDto;
        }

        public ResultDetailsDto? GetResultDetailsById(int resultId)
        {
            Result? result = _resultRepository.GetResultDetailsById(resultId);
            if (result == null)
            {
                throw new EntityDoesNotExist($"Result with id {resultId} does not exist.");
            }

            if (result.QuizAttempt == null)
            { return null; }

            List<ResultDetailsQuizAttemptAnswerDto> attemptAnswers = [];
            foreach (AttemptAnswer aa in result.QuizAttempt.AttemptAnswers)
            {
                if (aa.Question is null)
                { return null; }


                List<ResultDetailsAttemptAnswerOptionDto> userAnswer = [];

                foreach (AttemptAnswerOption ao in aa.AttemptAnswerOptions)
                {

                    if (ao.AnswerOption is null)
                    { return null; }

                    userAnswer.Add(new ResultDetailsAttemptAnswerOptionDto
                    (
                        ao.AnswerOption.Text,
                        ao.AnswerOption.IsCorrect
                    ));

                }
                ResultDetailsQuizAttemptAnswerDto attemptAnswer = new(
                    aa.Question.Text,
                    userAnswer,
                    aa.IsCorrect
                );

                attemptAnswers.Add(attemptAnswer);
            }

            return new ResultDetailsDto(
                result.Id,
                result.QuizAttemptId,
                result.QuizTitle,
                result.TotalQuestions,
                result.CorrectAnswers,
                result.Score,
                result.Percentage,
                result.TimeTakenMin,
                result.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                attemptAnswers


                );
        }

        public ResultDto CreateResult(Result result)
        {
           _resultRepository.CreateResult(result);
            return new ResultDto
                (
                    result.Id,
                    result.QuizAttemptId,
                    result.QuizTitle,
                    result.TotalQuestions,
                    result.CorrectAnswers,
                    result.Score,
                    result.Percentage,
                    result.TimeTakenMin,
                    result.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                );
        }
    }
}
