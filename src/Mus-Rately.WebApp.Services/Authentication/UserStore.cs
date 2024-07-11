using Microsoft.AspNetCore.Identity;
using Mus_Rately.WebApp.Domain.Models;
using Mus_Rately.WebApp.Repositories.Interfaces;

namespace Mus_Rately.WebApp.Services.Authentication
{
    public class UserStore : IUserPasswordStore<User>, IUserEmailStore<User>, IUserRoleStore<User>
    {
        private readonly IMusRatelyUnitOfWork _uow;


        public UserStore(IMusRatelyUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var usersRepository = _uow.GetRepository<User>();

            usersRepository.Add(user);
            await _uow.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var usersRepository = _uow.GetRepository<User>();

            usersRepository.Update(user);
            await _uow.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            var usersRepository = _uow.GetRepository<User>();

            usersRepository.Remove(user);
            await _uow.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;

            return Task.CompletedTask;
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var usersRepository = _uow.GetRepository<User>();

            var user = await usersRepository.GetSingleOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var usersRepository = _uow.GetRepository<User>();

            var user = await usersRepository.GetSingleOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName);

            return user;
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;

            return Task.CompletedTask;
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;

            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;

            return Task.CompletedTask;
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var usersRepository = _uow.GetRepository<User>();

            var user = await usersRepository.GetSingleOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);

            return user;
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;

            return Task.CompletedTask;
        }

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var userRoleRepository = _uow.GetRepository<UserRole>();
            var roleRepository = _uow.GetRepository<Role>();

            var role = await roleRepository.GetSingleOrDefaultAsync(r => r.NormalizedName == roleName);

            if (role == null) return;

            userRoleRepository.Add(new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
            });
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var userRoleRepository = _uow.GetRepository<UserRole>();

            var userRole = await userRoleRepository.GetSingleOrDefaultAsync(ur => ur.UserId == user.Id && ur.Role.NormalizedName == roleName);
            if (userRole != null)
            {
                userRoleRepository.Remove(userRole);
            }
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            var userRoleRepository = _uow.GetRepository<UserRole>();

            var userRolesForUser = await userRoleRepository.GetWhereAsync(ur => ur.UserId == user.Id);
            var roles = userRolesForUser.Select(userRole => userRole.Role.Name).ToList();

            return roles;
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var userRoleRepository = _uow.GetRepository<UserRole>();

            var isUserInRole = await userRoleRepository.AnyAsync(ur => ur.UserId == user.Id && ur.Role.NormalizedName == roleName);

            return isUserInRole;
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            var userRepository = _uow.GetRepository<User>();

            var users = await userRepository.GetWhereAsync(u => u.Roles.Any(ur => ur.Role.NormalizedName == roleName));

            return users.ToList();
        }

        public void Dispose()
        {

        }
    }
}
