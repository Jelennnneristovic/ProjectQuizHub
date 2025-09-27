using QuizHubDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubInfrastructure.Interfaces
{
    public interface IResultRepository
    {
        List<Result>GetResults();
        List<Result>GetResults(int UserId);
        Result? GetResultDetailsById(int resultId);
        void CreateResult(Result result);
        List<Result> GetLeaderboard(int quizId, string? period);
        List<Result> GetResultsByQuizIdAndUserIdOrderByCreatedAt(int quizId, int userId);

    }
}
