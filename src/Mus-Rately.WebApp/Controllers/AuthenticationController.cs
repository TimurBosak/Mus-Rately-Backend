using Microsoft.AspNetCore.Mvc;
using Mus_Rately.WebApp.Domain.Models;
using Mus_Rately.WebApp.DTO;
using Mus_Rately.WebApp.Services.Interfaces;

namespace Mus_Rately.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IRegisterService _registerService;


        public AuthenticationController(IRegisterService registerService)
        {
            _registerService = registerService;
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserDTO user)
        {
            var domainUser = new User
            {
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
            };

            var result = await _registerService.RegisterAsync(domainUser, user.Password);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
