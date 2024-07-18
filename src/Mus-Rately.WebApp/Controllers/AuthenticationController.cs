using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mus_Rately.WebApp.Domain.Models;
using Mus_Rately.WebApp.DTO;
using Mus_Rately.WebApp.Services.Interfaces;
using System.Security.Claims;

namespace Mus_Rately.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly ILoginService _loginService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;


        public AuthenticationController(IRegisterService registerService, ILoginService loginService, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _registerService = registerService;
            _loginService = loginService;
            _signInManager = signInManager;
            _userManager = userManager;
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

        [HttpGet]
        [Route("Google-Login")]
        public IActionResult GoogleAuthentication()
        {
            var postLoginUrl = Url.Action("ExternalLoginCallBack", "Authentication");
            var authProperties = _signInManager.ConfigureExternalAuthenticationProperties("Google", postLoginUrl);
            
            return new ChallengeResult("Google", authProperties);
        }

        [HttpGet]
        [Route("Google-Callback")]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            var callBackUrl = Url.Action("GetAllSongs", "Song");
            if (info == null)
            {
                return BadRequest();
            }

            var signInResult = await _loginService.GoogleSignInAsync(info);

            if (signInResult.Succeeded)
            {
                return Redirect(callBackUrl);
            }
            else
            {
                await _loginService.CreateGoogleUserInfoAsync(info);
                
                return Redirect(callBackUrl);
            }

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
