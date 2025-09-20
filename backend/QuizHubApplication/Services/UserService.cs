using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizHubApplication.Configuration;
using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.DTOs.Responses;
using QuizHubApplication.Exceptions;
using QuizHubApplication.Interfaces;
using QuizHubDomain.Entities;
using QuizHubInfrastructure.Interfaces;
using QuizHubInfrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PasswordVerificationResult = Microsoft.AspNetCore.Identity.PasswordVerificationResult;

namespace QuizHubApplication.Services
{
    public class UserService(IUserRepository userRepository, IOptions<AppSettings> options) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
       private readonly IOptions<AppSettings> _options = options;

        public UserDto CreateUser(CreateUserDto createUserDto)
        {

            User? user = _userRepository.GetUserByEmailOrUsername(createUserDto.Username, createUserDto.Email);

            if (user is not null)

            {
                throw new EntityAlreadyExists(string.Format("Registration failed! The user '{0}' already exists.", createUserDto.Username));
            }

            

            var hashedPasswords = new PasswordHasher<User>().HashPassword(new User(), createUserDto.Password);

            User newUser = new(createUserDto.Username,createUserDto.Email, hashedPasswords, createUserDto.AvatarUrl);
            _userRepository.CreateUser(newUser);
            return new UserDto(newUser.UserName, newUser.Email,  newUser.AvatarUrl);

            

        }

        public string Login(LoginUserDto loginUserDto)
        {
            User? user = _userRepository.GetUserByEmailOrUsername(loginUserDto.UserKey);


            //user ne postoji
            if (user is null)

            {
                throw new EntityDoesNotExist(string.Format("Login failed! The user with key '{0}' does not exist.", loginUserDto.UserKey));
            }


            //ako postoji, mora da bude validna sifra
            //poredjenje hash
            var hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(
                user: new User(),
                hashedPassword: user.PasswordHash, //sacuvan registracijom u bazi, od usera
                providedPassword: loginUserDto.Password //salje se u toku logina
            );

            if (result != PasswordVerificationResult.Success)
            {
                throw new InvalidPassword("Password is not correct");
            }

            return CreateToken(user);


        }

        private string CreateToken(User user)
        {
            //sadrzaj tokena
            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Name, user.UserName.ToString()),
                new (ClaimTypes.Email, user.Email.ToString()),
                new (ClaimTypes.Role, user.Role.ToString()),

            };

           var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Token));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(

                issuer: _options.Value.Issuer,
                audience: _options.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        
        }
    }
}
