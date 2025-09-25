using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.DTOs.Responses
{
    public record LeaderBoardEntryDto(      
         int Position,
         string Username,
         int Score,
         int TimeElapsedMin,
         string CompletedAt)
    {
    }
}
