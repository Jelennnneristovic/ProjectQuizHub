using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Requests
{
    public record CreateAnswerOptionDto(
        string Text,
        bool IsCorrect

        ) : ICRUDAnswerOptionDto
    {
        bool ICRUDAnswerOptionDto.IsCorrect()
        {
           return IsCorrect;
        }
    }
}
