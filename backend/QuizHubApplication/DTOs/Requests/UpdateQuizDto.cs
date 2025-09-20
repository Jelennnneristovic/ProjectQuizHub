using QuizHubDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Requests
{
    public record UpdateQuizDto(int id, string NewTitle, string? Description, int TimeLimit, string DifficultyLevel, int IdCategory)
    {
    }
}
