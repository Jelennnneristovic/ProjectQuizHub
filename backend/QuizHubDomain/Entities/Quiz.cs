using QuizHubDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubDomain.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int TimeLimit { get; set; } 

        public DifficultyLevel DifficultyLevel { get; set; }

        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Question> Questions { get; set; } = [];
        public List<QuizAttempt> QuizAttempts { get; set; } = [];

         

        public Quiz() { }
        public Quiz(string Title, string? Description, int TimeLimit, DifficultyLevel DifficultyLevel, int CategoryId)
        {
            this.Title = Title;
            this.Description = Description;
            this.TimeLimit = TimeLimit;
            this.DifficultyLevel = DifficultyLevel;
            this.CategoryId = CategoryId;
            this.IsActive = true;

        }





    }
}
