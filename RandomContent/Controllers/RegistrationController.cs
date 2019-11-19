using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RandomContent.Entities;
using RandomContent.Services;

namespace RandomContent.Controllers
{
    [Authorize]
    [ApiController]
    [Route("registration")]
    public class RegistrationController : ControllerBase
    {
        private IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]User userParam)
        {
            var user = _registrationService.Register(userParam);
            return Ok(user);
        }

        //[HttpGet]
        //[Route("perfectdog")]
        //public IActionResult GetPerfectDog()
        //{
        //    var dog = _contentService.GetPerfectDog();
        //    return Ok(dog);
        //}

    }
}