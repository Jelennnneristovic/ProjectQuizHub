using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubDomain.Entities
{
    public class AttemptAnswerOption
    {
        public int Id { get; set; }
        public int AnswerOptionId { get; set; }
        public AnswerOption? AnswerOption { get; set; }

        public int AttemptAnswerId { get; set; }
        public AttemptAnswer? AttemptAnswer { get; set; }
        public AttemptAnswerOption() { }
        public AttemptAnswerOption( int AnswerOptionId)
        {
            this.AnswerOptionId = AnswerOptionId;
        }
    }
}
