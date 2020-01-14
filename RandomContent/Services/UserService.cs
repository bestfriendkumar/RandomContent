using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RandomContent.Database.Context;
using RandomContent.Entities;
using RandomContent.Helpers;

namespace RandomContent.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }

    public class UserService : IUserService
    {

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            //Retrieve user from db
            User user;
            using (var db = new AppDbContext())
            {
                user = db.Users.SingleOrDefault(x => x.Username == username && x.Password == password);
            }

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();

            //Retrieve signing key from appSettings
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Create claims
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()), 
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;
            return user;
        }

        /// <summary>
        /// Retrieves all users and removes the password
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAll()
        {
            //Retrieve all users from db
            using (var db = new AppDbContext())
            {
                var users = db.Users.ToList().Select(x => {
                    x.Password = null;
                    return x;
                });
                return users;
            }
        }
    }
}