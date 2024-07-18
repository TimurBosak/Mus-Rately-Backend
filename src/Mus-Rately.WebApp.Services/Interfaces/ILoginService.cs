using Microsoft.AspNetCore.Identity;
using Mus_Rately.WebApp.Domain.Models;

namespace Mus_Rately.WebApp.Services.Interfaces
{
    public interface ILoginService
    {
        public Task LoginAsync(User user);

        public Task LoginAsync(string username, string password);

        public Task<SignInResult> GoogleSignInAsync(ExternalLoginInfo info);

        public Task CreateGoogleUserInfoAsync(ExternalLoginInfo info);

        public Task LogoutAsync();
    }
}
