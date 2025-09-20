using QuizHubDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubDomain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public string? AvatarUrl { get; set; }

        public List<QuizAttempt> quizAttempts = []; 

        public Role Role{ get; set; } = Role.User;
        public User() { }

        public User(string UserName, string Email, string PasswordHash, string? AvatarUrl)
        {
            this.UserName = UserName;
            this.Email = Email;
            this.PasswordHash = PasswordHash;
            this.AvatarUrl = AvatarUrl;
        }



    }
}
