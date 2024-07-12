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
        public async Task<IActionResult> ExternalLogin()
        {
            var stuff = await _signInManager.GetExternalAuthenticationSchemesAsync();
            var redirectUrl =  Url.Action("GetAllSongs", "Song");
            // Configure the redirect URL, provider and other properties
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", "https://localhost:7247/api/Authentication/Google-Login-Yeah");
            //This will redirect the user to the external provider's login page
            return new ChallengeResult("Google", properties);
        }

        [HttpGet]
        [Route("Google-Login-Yeah")]
        public async Task<IActionResult>
            ExternalLoginCallback()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return BadRequest();
            }

            // If the user already has a login (i.e if there is a record in AspNetUserLogins
            // table) then sign-in the user with this external login provider
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return Redirect("https://localhost:7247/api/Song");
            }
            // If there is no record in AspNetUserLogins table, the user may not have
            // a local account
            else
            {
                // Get the email claim value
                var email = info.Principal.FindFirst(ClaimTypes.Email).Value;

                if (email != null)
                {
                    // Create a new user without password if we do not have a user already
                    var user = await _userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new User
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Name = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            PasswordHash = "123"
                        };

                        await _userManager.CreateAsync(user);
                    }

                    // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                    await  _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return Redirect("https://localhost:7247/api/Song");
                }
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
