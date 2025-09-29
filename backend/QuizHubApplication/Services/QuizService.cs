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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuizHubApplication.Services
{
    public class QuizService(IQuizRepository quizRepository, ICategoryService categoryService) : IQuizService
    {
        private readonly IQuizRepository _quizRepository = quizRepository;
        private readonly ICategoryService _categoryService = categoryService;
        public QuizDto CreateQuiz(CreateQuizDto createQuizDto)
        {
            CategoryDto? category = _categoryService.GetCategory(createQuizDto.CategoryId);

            if (category is null)
            {
                throw new EntityDoesNotExist(string.Format("The category '{0}' does not exists.", createQuizDto.CategoryId.ToString()));
            }


            Quiz newQuiz = new(createQuizDto.Title, createQuizDto.Description,createQuizDto.TimeLimit, Enum.Parse<DifficultyLevel>(createQuizDto.DifficultyLevel), createQuizDto.CategoryId);
            _quizRepository.CreateQuiz(newQuiz);

         
            return new QuizDto(newQuiz.Id,
                category.Name,
                category.Description,
                newQuiz.Title,
                newQuiz.Description,
                newQuiz.TimeLimit,
                newQuiz.DifficultyLevel.ToString(),
                0);
        }

        public string DeleteQuiz(int id)
        {
            Quiz? quiz = _quizRepository.GetQuiz(id);

            //greska, brise nesto sto ne postoji
            if (quiz is null)
            {
                throw new EntityDoesNotExist(string.Format("The quiz '{0}' does not exists.", id.ToString()));
            }
            // ako nije null brise ga
            _quizRepository.DeleteQuiz(quiz);
            return string.Format("The quiz '{0}' is deleted .", quiz.Id);

        }



        public QuizDetailsDto? GetQuiz(int id)
        {
            Quiz? quiz = _quizRepository.GetQuiz(id);
            if (quiz is null)
            { return null; }

            List<QuestionDto> questions = [];
            foreach (Question question in quiz.Questions)
            { 
                List<AnswerOptionDto> answers = [];
                foreach (AnswerOption answerOption in question.AnswerOptions)
                {
                    answers.Add(new AnswerOptionDto(answerOption.Id, answerOption.Text, answerOption.IsCorrect));
                }

                questions.Add(new QuestionDto(
                    question.Id,
                    question.Text,
                    question.Points,
                    question.QuestionType.ToString(),
                    question.CorrectFillInAnswer,
                    answers
                ));

            }
            return new QuizDetailsDto(
                quiz.Id,
                quiz.Title,
                quiz.Description,
                quiz.TimeLimit,
                quiz.DifficultyLevel.ToString(),
                quiz.Category != null ? quiz.Category.Name : "",
                quiz.Category != null ? quiz.Category.Description : "",
                questions
            );

        }

        public Quiz? GetQuizById(int id)
        {
            return _quizRepository.GetQuiz(id);
        }

        //svi kvizovi +  category + aktivna pitanja (u bazi filtriram) da bih dosla do counta
        public List<QuizDto> GetQuizzes()
        {
            List <Quiz> quizzes= _quizRepository.GetQuizzes();
            List<QuizDto> result = [];

            foreach (Quiz quiz in quizzes)
            {
                QuizDto quizDto = new(
                    quiz.Id,
                    quiz.Category != null ? quiz.Category.Name : "",
                    quiz.Category != null ? quiz.Category.Description : null,
                    quiz.Title,
                    quiz.Description,
                    quiz.TimeLimit,
                    quiz.DifficultyLevel.ToString(),
                    quiz.Questions != null ? quiz.Questions.Count : 0
                );

                result.Add(quizDto);
            }

            return result;

        }

        public List<QuizDto> GetQuizzesByKeyWord(string keyword)
        {
            List<Quiz> quizzes = _quizRepository.GetQuizzesWithAllDetailsForKeyWord();
            List<QuizDto> result = [];

            foreach (Quiz quiz in quizzes)
            {
                if ((quiz.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                    (quiz.Description != null && quiz.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                    (quiz.Category != null &&
                    quiz.Category.Name != null && quiz.Category.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                    (quiz.Category != null && quiz.Category.Description != null && quiz.Category.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                    (quiz.Questions != null && quiz.Questions.Any(q => q.Text != null && q.Text.Contains(keyword, StringComparison.OrdinalIgnoreCase))) ||
                    (quiz.Questions != null && quiz.Questions.Any(q => q.AnswerOptions != null && q.AnswerOptions.Any(a => a.Text != null && a.Text.Contains(keyword, StringComparison.OrdinalIgnoreCase))))
                   )
                {
                    QuizDto quizDto = new(
                        quiz.Id,
                        quiz.Category != null && quiz.Category.Name != null ? quiz.Category.Name : "",
                        quiz.Category != null ? quiz.Category.Description : "",
                        quiz.Title,
                        quiz.Description,
                        quiz.TimeLimit,
                        quiz.DifficultyLevel.ToString(),
                        quiz.Questions != null ? quiz.Questions.Count : 0
                    );
                    result.Add(quizDto);
                }
            }
            return result;
        }

        public List<QuizDto> GetQuizzesSearch(DifficultyLevel? difficultyLevel, string? categoryName)
        {
            List<Quiz> quizzes = _quizRepository.GetQuizzesSearch(difficultyLevel,categoryName);
            
            
            List<QuizDto> result = [];
            foreach (Quiz quiz in quizzes)
            {
                QuizDto quizDto = new(
                    quiz.Id,
                    quiz.Category != null ? quiz.Category.Name : "",
                    quiz.Category != null ? quiz.Category.Description : null,
                    quiz.Title,
                    quiz.Description,
                    quiz.TimeLimit,
                    quiz.DifficultyLevel.ToString(),
                    quiz.Questions != null ? quiz.Questions.Count : 0
                );
                result.Add(quizDto);
            }
            return result;
        }

        public QuizDto? UpdateQuiz(UpdateQuizDto updateQuizDto)
        {

            //nije ga pronasao, ovde su mi svi podaci tog objekta
            Quiz? quiz = _quizRepository.GetQuiz(updateQuizDto.id);

            if (quiz is null)

            { 
                throw new EntityDoesNotExist(string.Format("The quiz '{0}' does not exists.", updateQuizDto.id.ToString())); 
            }

            // update polje samo ako je IsNullOrWhiteSpace, inace ostavlja staru vrednost

            if (!string.IsNullOrWhiteSpace(updateQuizDto.NewTitle))
                quiz.Title = updateQuizDto.NewTitle;

            if (!string.IsNullOrWhiteSpace(updateQuizDto.Description))
                quiz.Description = updateQuizDto.Description;

            if (!string.IsNullOrWhiteSpace(updateQuizDto.DifficultyLevel))
                quiz.DifficultyLevel =updateQuizDto.DifficultyLevel != null ? Enum.Parse<DifficultyLevel>(updateQuizDto.DifficultyLevel) : quiz.DifficultyLevel;

            //0 salje serveru kao znak da nije promenjeno.
            if (updateQuizDto.TimeLimit != 0 && updateQuizDto.TimeLimit != quiz.TimeLimit)
            {
                quiz.TimeLimit = updateQuizDto.TimeLimit;
            }

            Category? category = _categoryService.GetCategoryById(updateQuizDto.IdCategory);
           
            if (updateQuizDto.IdCategory != 0 && updateQuizDto.IdCategory != quiz.CategoryId)
            {
               
                if (category is null)
                {
                    throw new EntityDoesNotExist(string.Format("The category '{0}' does not exists.", updateQuizDto.IdCategory.ToString()));
                }
                quiz.CategoryId = updateQuizDto.IdCategory;
                quiz.Category = category;
                
            }


            _quizRepository.UpdateQuiz(quiz);

            return new QuizDto(quiz.Id, category !=null ? category.Name : "", 
                category != null ? category.Description : "", quiz.Title, quiz.Description, quiz.TimeLimit,
                quiz.DifficultyLevel.ToString(), quiz.Questions !=null ? quiz.Questions.Count:0);
        }

      
    }
}
