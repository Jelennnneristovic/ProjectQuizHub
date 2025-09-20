using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubDomain.Entities
{
    public class AnswerOption
    {
        public int Id { get; set; }
       
        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }

        public bool IsActive { get; set; }

        public Question? Question { get; set; }
        public int QuestionId { get; set; }


        public AnswerOption() { }
        public AnswerOption(string Text, bool IsCorrect, int QuestionId)
        {
            this.Text = Text;
            this.QuestionId = QuestionId;
            this.IsCorrect = IsCorrect;
            this.IsActive = true;

        }


    }
}
