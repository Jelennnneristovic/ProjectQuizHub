using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.DTOs.Responses;
using QuizHubApplication.Exceptions;
using QuizHubApplication.Interfaces;
using QuizHubDomain.Entities;

namespace QuizHubApi.Controllers
{
    [Route("api/quizAttempts")]
    [ApiController]
    public class QuizAttemptController(IQuizAttemptService quizAttemptService) : ControllerBase
    {
        private readonly IQuizAttemptService _quizAttemptService = quizAttemptService;

        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult<QuizAttemptDto> CreateQuizAttempt([FromBody] CreateQuizAttemptDto createQuizAttemptDto)
        {
            try
            {
                return Ok(_quizAttemptService.CreateQuizAttemptDto(createQuizAttemptDto));
            }
            catch (EntityDoesNotExist ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [HttpPut("{id}")]
        public ActionResult<QuizResultDto> FinishQuizAttempt(int id)
        {
            try
            {

                return Ok(_quizAttemptService.FinishQuizAttempt(id));
            }
            catch (Exception ex) when (ex is EntityDoesNotExist or QuizAttemptAlreadyFinished)
            {
                return BadRequest(ex.Message);

            }
        }

        //  [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<QuizAttemptDto>> GetAllQuizAttempts()
        {
            return Ok(_quizAttemptService.GetAllQuizAttempts());
        }

        //  [Authorize(Roles = "Admin")]
        [HttpGet("users/{userId}")]
        public ActionResult<List<QuizAttemptDto>> GetQuizAttemptsByUserId(int userId)
        {
            try
            {   
                return Ok(_quizAttemptService.GetQuizAttemptsByUserId(userId));
            }
            catch (EntityDoesNotExist ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Authorize(Roles = "User")]
        [HttpGet("user")]
        public ActionResult<List<QuizAttemptDto>> GetQuizAttemptsFromUser()
        {
            try
            {
                return Ok(_quizAttemptService.GetQuizAttemptsFromUser());
            }
            catch (EntityDoesNotExist ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}


