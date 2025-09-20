using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.DTOs.Responses;
using QuizHubApplication.Exceptions;
using QuizHubApplication.Interfaces;
using QuizHubDomain.Entities;
using QuizHubDomain.Enums;
using QuizHubInfrastructure.Interfaces;
using QuizHubInfrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Services
{
    public class QuestionService(IQuestionRepository questionRepository, IQuizService quizService, IAnswerOptionRepository answerOptionRepository) : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository = questionRepository;
        private readonly IQuizService _quizService = quizService;
        private readonly IAnswerOptionRepository _answerOptionRepository = answerOptionRepository;

        public QuizDetailsDto? CreateQuestionDto(CreateQuestionDto createQuestionDto)
        {
           Quiz? quiz = _quizService.GetQuizById(createQuestionDto.QuizId);
            if (quiz is null)
            {

                throw new EntityDoesNotExist(string.Format("Quiz {0} does not exist", createQuestionDto.QuizId));
            }

            ValidateAnswersWithQuestionType(createQuestionDto.QuestionType,
                createQuestionDto.CorrectFillInAnswer,
                [.. createQuestionDto.AnswerOptions.Cast<ICRUDAnswerOptionDto>()]);

            Question newQestion= new(createQuestionDto.Order, createQuestionDto.QuizId, createQuestionDto.Text, createQuestionDto.Points, createQuestionDto.QuestionType, createQuestionDto.CorrectFillInAnswer);

            _questionRepository.CreateQuestion(newQestion);
            List<AnswerOption> answerOptions = [];

            foreach (CreateAnswerOptionDto dto in createQuestionDto.AnswerOptions)
            { 
                answerOptions.Add(new AnswerOption(
                    dto.Text,
                    dto.IsCorrect,
                    newQestion.Id
                ));
            }

            _answerOptionRepository.CreateAnswerOption(answerOptions);


            return _quizService.GetQuiz(createQuestionDto.QuizId);

          
        }

        public QuizDetailsDto? DeleteQuestion(int quizId, int questionId)
        {
            //i kviz zbog quizdetails.
            Quiz? quiz = _quizService.GetQuizById(quizId);
            //greska, brise nesto sto ne postoji
            if (quiz == null)
            {
                throw new EntityDoesNotExist(string.Format("Quiz with {0} does not exist", quizId));
            }
            // ako nije null brise ga

            Question? question = _questionRepository.GetQuestionWithAnswers(questionId);

            //question ne pripada tom kvizu ili je question null
            if (question == null || question.QuizId != quizId)
            {
                throw new EntityDoesNotExist(string.Format("Question with {0} does not exist in quiz {1}", questionId, quizId));
            }
            _questionRepository.DeleteQuestion(question);
            return _quizService.GetQuiz(quizId);
        }

        public QuestionDto? GetQuestion(int number)
        {
            Question? question = _questionRepository.GetQuestionWithAnswers(number);
            if (question is null)

            { return null; }

            return new QuestionDto(0, question.Order, question.Text, question.Points, "", "", null);
        }

        public QuizDetailsDto? UpdateQuestion(UpdateQuestionDto updateQuestionDto)
        {
            //azuriramo odgovore od onih koji su aktivni
            //azuriram samo one koji su menjani

         
            //nije pga pronasao, ovde su mi svi podaci tog objekta
            Quiz? quiz = _quizService.GetQuizById(updateQuestionDto.QuizId);

            if (quiz is null)
            {
                throw new EntityDoesNotExist(string.Format("Quiz with {0} does not exist", updateQuestionDto.QuizId));
            }

            Question? question = _questionRepository.GetQuestionWithAnswers(updateQuestionDto.QuestionId);

            if (question is null || question.QuizId != updateQuestionDto.QuizId)

            {
                throw new EntityDoesNotExist(string.Format("Question with {0} does not exist", updateQuestionDto.QuestionId));
            }

            // update polje samo ako je IsNullOrWhiteSpace, inace ostavlja staru vrednost

            if (!string.IsNullOrWhiteSpace(updateQuestionDto.Text))
                question.Text = updateQuestionDto.Text;


            if (updateQuestionDto.Points > 0 )
            {
                question.Points = updateQuestionDto.Points;
            }


            foreach (AnswerOption ao in question.AnswerOptions)
            { 
            
                foreach (UpdateAnswerOptionDto dto in updateQuestionDto.UpdateAnswerOptionDtos)
                {
                    if (ao.Id == dto.AnswerOptionId)
                    {
                        if (!string.IsNullOrWhiteSpace(dto.Text))
                        {
                            ao.Text = dto.Text;
                        }
                        ao.IsCorrect = dto.IsCorrect;
                    }
                }

            }

            _questionRepository.UpdateQuestion(question);
            return _quizService.GetQuiz(updateQuestionDto.QuizId);
        }
        private void ValidateAnswersWithQuestionType(QuestionType QuestionType, string? CorrectFillAnswer, List<ICRUDAnswerOptionDto> AnswerOptionDtos)
        {
            if (QuestionType.Equals(QuestionType.FillIn))
            {
                if (CorrectFillAnswer is null || AnswerOptionDtos.Count != 1)
                { throw new NotFillInTypeQuestion(); }


            } 
            else if (QuestionType.Equals(QuestionType.SingleChoice))
            {
                if (AnswerOptionDtos.Count < 2 || AnswerOptionDtos.Count(a => a.IsCorrect()) != 1)
                { throw new SingleCgoiceQuestionWithMultipleCorrectAnswers(); }
            }

            /*
             * DODATI OSTALE VALIDACIJE KAO STO SU:
             * 
            if (string.IsNullOrWhiteSpace(question.Text))
            {
                throw new InvalidEntityException("Question text cannot be empty.");
            }
            if (question.Points < 0)
            {
                throw new InvalidEntityException("Points must be a positive integer.");
            }
            // Additional validations can be added here as needed
            */


        }
    }
}
