using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubDomain.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public int QuizAttemptId { get; set; }
        public QuizAttempt? QuizAttempt { get; set; }
        public string QuizTitle { get; set; } = string.Empty;

        // Polja rezultata
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int Score { get; set; }
        public double Percentage { get; set; }

        public int TimeTakenMin { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Result() { }


        public Result(int QuizAttemptId, string QuizTitle, int TotalQuestions, int CorrectAnswers, int Score, double Percentage, int TimeTakenMin)
        {
            this.QuizAttemptId = QuizAttemptId;
            this.QuizTitle = QuizTitle;
            this.TotalQuestions = TotalQuestions;
            this.CorrectAnswers = CorrectAnswers;
            this.Score = Score;
            this.Percentage = Percentage;
            this.TimeTakenMin = TimeTakenMin;
        }

    }

}
