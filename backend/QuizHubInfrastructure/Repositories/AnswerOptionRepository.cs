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
    public class AnswerOptionRepository(QuizHubDbContext context) : IAnswerOptionRepository
    {
        private readonly QuizHubDbContext _context = context;
        public void CreateAnswerOption(List<AnswerOption> answerOptions)
        {
            _context.AnswerOptions.AddRange(answerOptions);
            _context.SaveChanges();

        }
    }
}
