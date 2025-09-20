using QuizHubDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubInfrastructure.Interfaces
{
    public interface IQuestionRepository
    {
        Question? GetQuestionWithAnswers(int questionId);

        Question CreateQuestion(Question question);

        // i pitanja i odgovore

        void DeleteQuestion(Question question);

        void UpdateQuestion(Question question);
    }
}
