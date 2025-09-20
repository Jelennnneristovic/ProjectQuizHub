using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubDomain.Entities
{
    public class QuizAttempt
    {

        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
        public int QuizId { get; set; }
        public Quiz? Quiz { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.Now;
        public int? TimeTakenSeconds { get; set; }
        public DateTime? FinishedAt { get; set; } 

        public int Score { get; set; } = 0;

        public List<AttemptAnswer> AttemptAnswers { get; set; } = [];
    
        public QuizAttempt() { }
        public QuizAttempt(int quizId, int userId) 
        {
            this.QuizId = quizId;
            this.UserId = userId;
        }
    }
}
