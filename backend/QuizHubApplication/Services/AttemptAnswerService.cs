using QuizHubApplication.DTOs.Requests;
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
    public class AttemptAnswerService(IAttemptAnswerRepository attemptAnswerRepository,IQuizService quizService ,IQuizAttemptService quizAttemptService, IQuestionService questionService): IAttemptAnswerService
    {
        private readonly IAttemptAnswerRepository _attemptAnswerRepository = attemptAnswerRepository;
        private readonly IQuizAttemptService _quizAttemptService = quizAttemptService;
        private readonly IQuizService _quizService = quizService;
        private readonly IQuestionService _questionService = questionService;

        public void CreateAttemptAnswer(CreateAttemptAnswerDto createAttemptAnswerDto)
        {
            Quiz? quiz = _quizService.GetQuizById(createAttemptAnswerDto.QuizId);
            if (quiz is null)
            {

                throw new EntityDoesNotExist(string.Format("Quiz {0} does not exist", createAttemptAnswerDto.QuizId));
            }

            QuizAttempt? quizAttempt = _quizAttemptService.GetQuizAttemptById(createAttemptAnswerDto.QuizAttemptId);
            
            if (quizAttempt is null || quizAttempt.QuizId != createAttemptAnswerDto.QuizId)
            {

                throw new EntityDoesNotExist(string.Format("Quiz attempt {0} does not exist", createAttemptAnswerDto.QuizAttemptId));
            }

            Question? question = _questionService.GetQuestionWithAnswers(createAttemptAnswerDto.QuestionId);

            if (question is null || question.QuizId != createAttemptAnswerDto.QuizId)
            {

                throw new EntityDoesNotExist(string.Format("Question attempt {0} does not exist", createAttemptAnswerDto.QuestionId));
            }

            List<AttemptAnswerOption> attemptAnswerOptions = [];
            foreach (int answerOptionId in createAttemptAnswerDto.AttemptAnswerOptions)
            { 
                attemptAnswerOptions.Add(new AttemptAnswerOption(answerOptionId));

            }

            //odgovori Usera
            AttemptAnswer newAttemptAnswer = new(
                    createAttemptAnswerDto.QuizAttemptId,
                    createAttemptAnswerDto.QuestionId,
                    createAttemptAnswerDto.FillInAnswer,
                    attemptAnswerOptions);

            if (question.QuestionType == QuestionType.SingleChoice || question.QuestionType == QuestionType.TrueFalse)
            {
                ValidateSingleCorrectAnswer(question, newAttemptAnswer);
            }
            else if (question.QuestionType == QuestionType.MultipleChoice)
            {
                ValidateMultipleCorrectAnswer(question, newAttemptAnswer);
            }
            else // znamo da je ovo case za fill in
            {
                ValidateFillInCorrectAnswer(question, newAttemptAnswer);
            }

            _attemptAnswerRepository.CreateAttemptAnswer(newAttemptAnswer);

            return; 
        }

        private void ValidateSingleCorrectAnswer(Question question,AttemptAnswer newAttemptAnswer)
        {
            //User je selektovao samo 1 odg
            if (newAttemptAnswer.AttemptAnswerOptions.Count !=1)
            {
                return; //nisu tacni odgovori ostaje false i 0 poena
            }
            int selectedOption = newAttemptAnswer.AttemptAnswerOptions.First().AnswerOptionId;

            //idemo kroz sve odgovore i proveravamo da li je korisnikov tacan
            foreach (AnswerOption aao in question.AnswerOptions)
            {
                if (aao.Id == selectedOption) // odgovor koji je korisnik izabrao
                {
                    if (aao.IsCorrect) //ako je taj odgovor tacan
                    {
                        newAttemptAnswer.IsCorrect = true;
                        newAttemptAnswer.AwardedPoints = question.Points;

                    }

                    return; //nista nije tacno ostaje false i 0 poena

                }
            }
        }
        private void ValidateMultipleCorrectAnswer(Question question, AttemptAnswer newAttemptAnswer)
        {
            
            if (newAttemptAnswer.AttemptAnswerOptions.Count < 2)
            {
                return;
            }
            //odgovor je tacan ako je korisnik odgovorio na sva pitanja tacno, i svi koji su tacni odgovori moraju biti oznacceni

            HashSet<int> correctAnswers = [];
            foreach (AnswerOption answerOption in question.AnswerOptions)
            {
                if (answerOption.IsCorrect && answerOption.IsActive)
                {
                    correctAnswers.Add(answerOption.Id);
                }
            }

            HashSet<int> userAnswers = [];
            foreach (AttemptAnswerOption option in newAttemptAnswer.AttemptAnswerOptions)
            {
                userAnswers.Add(option.AnswerOptionId);
            }

            if (correctAnswers.SetEquals(userAnswers))
            {
                newAttemptAnswer.IsCorrect = true;
                newAttemptAnswer.AwardedPoints = question.Points;
            }
        }
        private void ValidateFillInCorrectAnswer(Question question, AttemptAnswer newAttemptAnswer)
        {
            if (question.CorrectFillInAnswer is null || newAttemptAnswer.FillInAnswer is null)
            {
                return;
            }
            if (string.Equals(question.CorrectFillInAnswer, newAttemptAnswer.FillInAnswer, StringComparison.OrdinalIgnoreCase))
            {
                //ako je tacan dogovor -> 
                newAttemptAnswer.IsCorrect = true;
                newAttemptAnswer.AwardedPoints = question.Points;
            }
        
        }
    }
}
