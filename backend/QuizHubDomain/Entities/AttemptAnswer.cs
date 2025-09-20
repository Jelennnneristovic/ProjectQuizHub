using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubDomain.Entities
{
    public class AttemptAnswer
    {
        public int Id { get; set; }

        public int AnswerOptionId { get; set; }
        public AnswerOption? AnswerOption { get; set; }
        public string? FillInAnswer { get; set; }

        public bool IsCorrect { get; set; } 
        public int AwardedPoints { get; set; }

        public int QuizAttemptId { get; set; }
        public QuizAttempt? QuizAttempt { get; set; }


    

        public AttemptAnswer() { }


    }
}
