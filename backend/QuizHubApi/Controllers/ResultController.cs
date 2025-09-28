using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizHubApplication.DTOs.Responses;
using QuizHubApplication.Interfaces;

namespace QuizHubApi.Controllers
{
    [Route("api/results")]
    [ApiController]
    public class ResultController(IResultService resultService) : ControllerBase
    {
        private readonly IResultService _resultService = resultService;

        //[Authorize(Roles = "Admin")]
        [HttpGet ("admin")]
        public ActionResult<List<ResultDto>> GetResults()
        {
            return Ok(_resultService.GetResults());
        }

        [Authorize(Roles = "User")]
        [HttpGet]

        public ActionResult<List<ResultDto>> GetResultsByUser()
        {
            return Ok(_resultService.GetResultsByUser());
        }

       // [Authorize(Roles = "User,Admin")]
        [HttpGet("details/{Id}")]
        public ActionResult<ResultDetailsDto?> GetResultDetailsById(int Id)
        {
            try {
                ResultDetailsDto? resultDetails = _resultService.GetResultDetailsById(Id);

                if (resultDetails is null)
                {
                    return BadRequest();
                }

                return Ok(resultDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("leaderboard")]
        public ActionResult<LeaderBoardEntriesDto> GetLeaderboard([FromQuery]  int QuizId, [FromQuery] string? Period)
        {
            try
            {
                LeaderBoardEntriesDto leaderBoardEntries = _resultService.GetLeaderboard(new (QuizId, Period));
                return Ok(leaderBoardEntries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
