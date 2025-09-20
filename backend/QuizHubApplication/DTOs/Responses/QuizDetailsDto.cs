using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Responses
{
    public record QuizDetailsDto(int Id, string Title, string? Description, int TimeLimit, string DifficultyLevel,string CategoryName, string? CategoryDescription,
        List<QuestionDto>Questions)
    {
    }
}
