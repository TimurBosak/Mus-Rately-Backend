using Microsoft.AspNetCore.Identity;
using Mus_Rately.WebApp.Domain.Models;
using Mus_Rately.WebApp.Repositories.Interfaces;
using Mus_Rately.WebApp.Services.Interfaces;

namespace Mus_Rately.WebApp.Services.Authentication
{
    public class RegisterService : IRegisterService
    {
        private readonly IMusRatelyUnitOfWork _uow;
        private readonly UserManager<User> _userManager;


        public RegisterService(IMusRatelyUnitOfWork uow, UserManager<User> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<bool> RegisterAsync(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(user.Email)) return false;
            if (string.IsNullOrWhiteSpace(user.Name)) return false;
            if (string.IsNullOrWhiteSpace(password)) return false;
            if (string.IsNullOrWhiteSpace(user.UserName)) return false;

            var userCreationIdentityResult = await _userManager.CreateAsync(user, password);
            if (userCreationIdentityResult.Succeeded)
            {
                var userRoleRepository = _uow.GetRepository<UserRole>();
                var isAnyUserAdmin = await userRoleRepository.AnyAsync(ur => ur.Role.Name == Role.Admin);
                if (!isAnyUserAdmin)
                {
                    var roles = new[] { Role.Admin, Role.User };
                    await _userManager.AddToRolesAsync(user, roles);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, Role.User);
                }

                return true;
            }

            return false;
        }

        public async Task<bool> CheckIfUserNameIsAvailableAsync(string userName)
        {
            var userRepository = _uow.GetRepository<User>();

            var duplicateUser = await userRepository.GetSingleOrDefaultAsync(u => u.UserName == userName);

            return duplicateUser == null; 
        }
    }
}
