using QuizHubApplication.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Interfaces
{
    public interface IAttemptAnswerService
    {
        void CreateAttemptAnswer(CreateAttemptAnswerDto createAttemptAnswerDto);
    }
}
