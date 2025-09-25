using QuizHubApplication.DTOs.Responses;
using QuizHubDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Interfaces
{
    public interface IResultService 
    {
        List<ResultDto> GetResults();
        List<ResultDto> GetResultsByUser();
        ResultDetailsDto? GetResultDetailsById(int resultId);
        ResultDto CreateResult(Result result);
    }
}
