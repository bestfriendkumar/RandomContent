using Microsoft.Extensions.Options;
using RandomContent.Entities;
using RandomContent.Helpers;

namespace RandomContent.Services
{
    public interface IRegistrationService
    {
        User Register(User user);
    }

    public class RegistrationService : IRegistrationService
    {

        private readonly AppSettings _appSettings;

        public RegistrationService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Insert user into memory and return and return success/failure
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User Register(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}