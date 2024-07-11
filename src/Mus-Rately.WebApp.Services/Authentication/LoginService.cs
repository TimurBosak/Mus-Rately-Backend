using Microsoft.AspNetCore.Identity;
using Mus_Rately.WebApp.Domain.Models;
using Mus_Rately.WebApp.Services.Interfaces;

namespace Mus_Rately.WebApp.Services.Authentication
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<User> _signInManager;


        public LoginService(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }


        public async Task LoginAsync(string userName, string password)
        {
            await _signInManager.PasswordSignInAsync(userName, password, true, false);
        }

        public async Task LoginAsync(User user)
        {
            await _signInManager.SignInAsync(user, true);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
