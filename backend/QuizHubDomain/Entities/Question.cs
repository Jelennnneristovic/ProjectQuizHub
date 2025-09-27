using QuizHubDomain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubDomain.Entities
{
    public class Question
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;
        public Quiz? Quiz { get; set; }
        public int QuizId { get; set; }


        
        public int Points { get; set; } = 0; //promenila na 0 umesto 1 

        public QuestionType QuestionType { get; set; } = QuestionType.SingleChoice;

        // za fillin tip u svim ostalim slucajevima je null vrednost fielda
        public string? CorrectFillInAnswer { get; set; }

        public bool IsActive { get; set; }

        public List<AnswerOption> AnswerOptions { get; set; } = [];

        public Question() { }

        //bodovi, vreme i tezina??
        public Question( string Text, int Points) 
        {
            this.Text = Text;
            this.Points = Points;
        }

        public Question(int QuizId,string Text, int Points, QuestionType questionType, string? correctFillInAnswer)
        {
            this.Text = Text;
            this.QuizId = QuizId;
            this.Points = Points;
            this.QuestionType = questionType;
            this.CorrectFillInAnswer = correctFillInAnswer;
            this.IsActive = true;
        }
    }

   


    }
