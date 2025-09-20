using QuizHubDomain.Entities;
using QuizHubInfrastructure.Data;
using QuizHubInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubInfrastructure.Repositories
{
    public class AttemptAnswerRepository(QuizHubDbContext context): IAttemptAnswerRepository
    {
        private readonly QuizHubDbContext _context = context;

        public void CreateAttemptAnswer(AttemptAnswer attemptAnswer)
        {
            _context.AttemptAnswers.Add(attemptAnswer);
            _context.SaveChanges();
        }
    }
}
