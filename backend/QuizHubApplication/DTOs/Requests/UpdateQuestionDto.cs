using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Requests
{
    public record UpdateQuestionDto(int QuizId, int QuestionId, string Text, int Points, List<UpdateAnswerOptionDto> UpdateAnswerOptionDtos)
    {
    }
}
