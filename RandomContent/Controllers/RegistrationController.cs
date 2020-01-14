using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RandomContent.Entities;
using RandomContent.Pages;
using RandomContent.Services;

namespace RandomContent.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Registration")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        /// <summary>
        /// Creates a new user
        /// Will return a bad request if the user could not be added to the db
        /// or the user already exists
        /// </summary>
        /// <param name="userParam"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegistrationModel userParam)
        {
            //Validations
            if (!Enum.IsDefined(typeof(Role), userParam.Role))
                return BadRequest("Must select valid Role for user");
            if (string.IsNullOrWhiteSpace(userParam.Username))
                return BadRequest("Must enter a username");
            if (string.IsNullOrWhiteSpace(userParam.Password))
                return BadRequest("Must enter a password");

            //Register user
            var user = _registrationService.Register(userParam);
            if (user != null) return Ok(user);
            return BadRequest("Could not create user profile. They username may have been taken, or something else happened.");
        }

    }
}