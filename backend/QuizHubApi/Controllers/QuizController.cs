using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.DTOs.Responses;
using QuizHubApplication.Exceptions;
using QuizHubApplication.Interfaces;
using QuizHubApplication.Services;
using QuizHubDomain.Enums;

namespace QuizHubApi.Controllers
{
    [Route("api/quizzes")]
    [ApiController]
    public class QuizController(IQuizService quizService) : ControllerBase
    {
        private readonly IQuizService _quizService = quizService;

        [HttpPost]
        public ActionResult<QuizDto> CreateQuiz([FromBody] CreateQuizDto createQuizDto)
        {

            return Ok(_quizService.CreateQuiz(createQuizDto));
        }

        //[HttpGet("{title}")]
        //public ActionResult<QuizDto> GetQuiz(string title)
        //{
        //    QuizDto? quizDto = _quizService.GetQuiz(title);

        //    if (quizDto is null)

        //    { return NotFound(string.Format("The quiz '{0}' does not exists.", title)); }

        //    return Ok(quizDto);
        //}

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteQuiz(int id)
        {
            try
            {
                return Ok(_quizService.DeleteQuiz(id));
            }
            catch (EntityAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<QuizDto> UpdateQuiz([FromBody] UpdateQuizDto updateQuizDto)
        {

            QuizDto? quizDto = _quizService.UpdateQuiz(updateQuizDto);

            if (quizDto is null)

            { return BadRequest(string.Format("The quiz '{0}' can not be updated! It does not exists.", updateQuizDto.id)); }


            return Ok(quizDto);


        }

        // [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        public ActionResult<List<QuizDetailsDto>> GetQuiz(int id)
        {
            QuizDetailsDto? quiz = _quizService.GetQuiz(id);
            if (quiz is null)
            {
                return NotFound(string.Format("The quiz with id '{0}' does not exists.", id.ToString()));
            }


            return Ok(quiz);

        }

        //  [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public ActionResult<List<QuizDto>> GetQuizzes()
        {

            return Ok(_quizService.GetQuizzes());

        }

        //  [Authorize(Roles = "Admin, User")]
        [HttpGet("search")]
        public ActionResult<List<QuizDto>> GetQuizzesSearch([FromQuery] DifficultyLevel? difficultyLevel, [FromQuery] string? categoryName)
        {

            return Ok(_quizService.GetQuizzesSearch(difficultyLevel, categoryName));

        }


        //  [Authorize(Roles = "Admin, User")]
        [HttpGet("search/{keyword}")]
        public ActionResult<List<QuizDto>> GetQuizzesByKeyWord(string keyword)
        {

            return Ok(_quizService.GetQuizzesByKeyWord(keyword));

        }

    }
}
