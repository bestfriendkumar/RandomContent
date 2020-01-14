using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RandomContent.Entities;
using RandomContent.Services;

namespace RandomContent.Controllers
{
    [Authorize]
    [ApiController]
    [Route("content")]
    public class ContentController : ControllerBase
    {
        private IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet]
        [Route("randomdog")]
        public IActionResult GetRandomDog()
        {
            var dog = _contentService.GetRandomDog();
            return Ok(dog);
        }

        /// <summary>
        /// End point where only certain users can access
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "PrivilegedUser")]
        [Route("perfectdog")]
        public IActionResult GetPerfectDog()
        {
            var dog = _contentService.GetPerfectDog();
            return Ok(dog);
        }
    }
}