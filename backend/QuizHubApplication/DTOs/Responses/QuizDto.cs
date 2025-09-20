using QuizHubDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Responses
{
    public record QuizDto(
        int Id, 
        string CategoryName, 
        string? CategoryDescription, 
        string Title, 
        string? QuizDescription, 
        int TimeLimit, 
        string DifficultyLevel, 
        int QuestionsCount
    )
    {
    }
}
