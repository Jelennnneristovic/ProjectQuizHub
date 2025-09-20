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
        public string? FillInAnswer { get; set; }
        public bool IsCorrect { get; set; } 
        public int AwardedPoints { get; set; }

        public int QuestionId { get; set; }
        public Question? Question { get; set; }

        public int QuizAttemptId { get; set; }
        public QuizAttempt? QuizAttempt { get; set; }

        public List<AttemptAnswerOption> AttemptAnswerOptions = [];

        public AttemptAnswer() { }
        public AttemptAnswer(int QuizAttemptId, int QuestionId, string? FillInAnswer, List<AttemptAnswerOption> AttemptAnswerOptions) 
        {
            this.QuizAttemptId = QuizAttemptId;
            this.QuestionId = QuestionId;
            this.FillInAnswer = FillInAnswer;
            this.AttemptAnswerOptions = AttemptAnswerOptions;
            this.IsCorrect = false;
            this.AwardedPoints = 0;

        }


    }
}
