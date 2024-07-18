using Microsoft.AspNetCore.Identity;
using Mus_Rately.WebApp.Domain.Models;
using Mus_Rately.WebApp.Services.Interfaces;
using System.Security.Claims;

namespace Mus_Rately.WebApp.Services.Authentication
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;


        public LoginService(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public async Task LoginAsync(string userName, string password)
        {
            await _signInManager.PasswordSignInAsync(userName, password, true, false);
        }

        public async Task LoginAsync(User user)
        {
            await _signInManager.SignInAsync(user, true);
        }

        public async Task<SignInResult> GoogleSignInAsync(ExternalLoginInfo info)
        {
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            return signInResult;
        }

        public async Task CreateGoogleUserInfoAsync(ExternalLoginInfo info)
        {
            var email = info.Principal.FindFirst(ClaimTypes.Email).Value;

            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    user = new User
                    {
                        UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                        Name = info.Principal.FindFirstValue(ClaimTypes.Email),
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };

                    await _userManager.CreateAsync(user);
                }

                await _userManager.AddLoginAsync(user, info);
                await _userManager.AddToRoleAsync(user, Role.User);
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
