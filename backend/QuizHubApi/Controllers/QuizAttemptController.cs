using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.DTOs.Responses;
using QuizHubApplication.Exceptions;
using QuizHubApplication.Interfaces;

namespace QuizHubApi.Controllers
{
    [Route("api/quizAttempt")]
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
    }
}


