using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Requests
{
    public record CreateAttemptAnswerDto(int QuizId, int QuizAttemptId,int QuestionId,string? FillInAnswer, List<int> AttemptAnswerOptions )
    {
        //AttemptAnswerOptions lista odgovora koje je korisnik  odabrao. + id Odgovora
    }
}
