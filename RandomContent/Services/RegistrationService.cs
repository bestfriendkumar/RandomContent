using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RandomContent.Database.Context;
using RandomContent.Entities;
using RandomContent.Helpers;
using RandomContent.Pages;

namespace RandomContent.Services
{
    public interface IRegistrationService
    {
        User Register(RegistrationModel user);
    }

    public class RegistrationService : IRegistrationService
    {

        private readonly AppSettings _appSettings;

        public RegistrationService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Insert user into memory and return and return the entity
        /// Failure if returns null
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User Register(RegistrationModel user)
        {
            try
            {
                //create db context
                using (var db = new AppDbContext())
                {
                    //check if user exists
                    var userExists = db.Users.FirstOrDefault(x => x.Username == user.Username) != null;
                    if (userExists)
                    {
                        //if they do exist, return null
                        return null;
                    }

                    //Create User Model
                    var newUser = new User()
                    {
                        Role = (Role)user.Role,
                        Username = user.Username,
                        Password = user.Password
                    };

                    //add user to db
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    //Null the password once it's stored in the db so that the user can't see it when it returned
                    user.Password = null;
                    return newUser;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}