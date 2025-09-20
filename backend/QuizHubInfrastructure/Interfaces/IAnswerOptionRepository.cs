using QuizHubDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubInfrastructure.Interfaces
{
    public interface IAnswerOptionRepository
    {
        void CreateAnswerOption(List<AnswerOption> answerOptions);
        
    }
}
