using Microsoft.AspNetCore.Http;
using QuizHubApplication.Configuration;
using QuizHubApplication.Interfaces;
using QuizHubDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Services
{
    public class TokenService(IHttpContextAccessor httpContextAccessor) : ITokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public UserContext GetUserContext()
        {
            return new UserContext(
                int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!),
                _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty,
                _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? string.Empty,
                 Enum.Parse<Role>(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role) ?? "User")
            );
        }
    }
}
