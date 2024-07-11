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
        private readonly ILoginService _loginService;


        public AuthenticationController(IRegisterService registerService, ILoginService loginService)
        {
            _registerService = registerService;
            _loginService = loginService;
        }


        [HttpPost]
        [Route("Register")]
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
                await _loginService.LoginAsync(domainUser);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            await _loginService.LoginAsync(userName, password);

            return Ok();
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _loginService.LogoutAsync();

            return Ok();
        }
    }
}
