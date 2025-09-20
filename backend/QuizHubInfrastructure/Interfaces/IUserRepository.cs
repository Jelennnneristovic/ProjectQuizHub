using QuizHubDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubInfrastructure.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        User? GetUserByEmailOrUsername(string username, string email);
        User? GetUserByEmailOrUsername(string userKey);
    }
}
