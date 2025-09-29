using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.Exceptions;
using QuizHubApplication.Interfaces;
using System.Runtime.InteropServices;

namespace QuizHubApi.Controllers
{
    [Route("api/attemptAnswers")]
    [ApiController]
    public class AttemptAnswerController(IAttemptAnswerService attemptAnswerService) : ControllerBase
    {
        private readonly IAttemptAnswerService _attemptAnswerService = attemptAnswerService;

        [HttpPost]
        public ActionResult<string> CreateAttemptAnswer([FromBody] CreateAttemptAnswerDto createAttemptAnswerDto)
        {
            try { 
                _attemptAnswerService.CreateAttemptAnswer(createAttemptAnswerDto);
                return Ok("Attempt answer created successfully");
            }
            catch (EntityDoesNotExist ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
    }
}
