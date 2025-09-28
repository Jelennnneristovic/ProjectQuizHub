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
    public class UserRepository(QuizHubDbContext context) : IUserRepository
    {
        private readonly QuizHubDbContext _context = context;

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        
        }

        public User? GetUserByEmailOrUsername(string username, string email)
        {

            return _context.Users.FirstOrDefault(u => u.UserName == username || u.Email == email);

        }

        public User? GetUserByEmailOrUsername(string userKey)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userKey || u.Email == userKey);
        }

        public User? GetUserById(int id)
        {
            return _context.Users.Where(u => u.Id == id)
                .Select(u => new User() { 
                    Id=u.Id,
                    UserName=u.UserName,
                    Email=u.Email,
                    AvatarUrl=u.AvatarUrl,

                
                }).FirstOrDefault();
        }
    }
}
