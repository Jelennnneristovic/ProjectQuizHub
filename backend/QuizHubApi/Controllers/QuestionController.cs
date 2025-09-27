using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.DTOs.Responses;
using QuizHubApplication.Exceptions;
using QuizHubApplication.Interfaces;
using QuizHubApplication.Services;

namespace QuizHubApi.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionController(IQuestionService questionService) : ControllerBase
    {
        private readonly IQuestionService _questionService = questionService;

        //[Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult<QuizDetailsDto> CreateQuestion( [FromBody] CreateQuestionDto createQuestionDto)
        {
            try
            {
                QuizDetailsDto? questionDto = _questionService.CreateQuestionDto(createQuestionDto);

                return Ok(questionDto);
            }
            catch (Exception ex) when (ex is EntityDoesNotExist or NotFillInTypeQuestion or SingleCgoiceQuestionWithMultipleCorrectAnswers)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        public ActionResult<QuizDetailsDto> DeleteQuestion([FromBody] DeleteQuestionDto deleteQuestionDto)
        {

            try {

                    QuizDetailsDto? questionDto = _questionService.DeleteQuestion(deleteQuestionDto.QuizId, deleteQuestionDto.QuestionId);
                    return Ok(questionDto);
                                
            }
            catch (EntityDoesNotExist ex)
            {
                return BadRequest(ex.Message);
            }
            /*
            QuestionDto? questionDto = _questionService.DeleteQuestion(order);
            if (questionDto is null)

            { return BadRequest(string.Format("The question '{0}' can not be deleted! It does not exists.", order)); }

            return Ok(questionDto)
                */
        }

        [HttpPut]
        public ActionResult<QuizDetailsDto> UpdateQuestion([FromBody] UpdateQuestionDto updateQuestionDto)
        {

         
            try
            {
 
                    return Ok(_questionService.UpdateQuestion(updateQuestionDto));

            }

            catch (EntityDoesNotExist ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
