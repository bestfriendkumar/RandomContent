using RandomContent.Entities;

namespace RandomContent.Services
{
    public interface IRegistrationService
    {
        User Register(string username, string password);
    }

    public class RegistrationService : IRegistrationService
    {
        public User Register(string username, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}