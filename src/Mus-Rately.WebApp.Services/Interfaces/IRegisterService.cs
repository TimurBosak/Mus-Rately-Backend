using Mus_Rately.WebApp.Domain.Models;

namespace Mus_Rately.WebApp.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<bool> RegisterAsync(User user, string password);

        Task<bool> CheckIfUserNameIsAvailableAsync(string userName);
    }
}
