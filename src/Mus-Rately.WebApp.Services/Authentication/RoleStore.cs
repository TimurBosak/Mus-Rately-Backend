using Microsoft.AspNetCore.Identity;
using Mus_Rately.WebApp.Domain.Models;
using Mus_Rately.WebApp.Repositories.Interfaces;

namespace Mus_Rately.WebApp.Services.Authentication
{
    public class RoleStore : IRoleStore<Role>
    {
        private readonly IMusRatelyUnitOfWork _uow;


        public RoleStore(IMusRatelyUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            var roleRepository = _uow.GetRepository<Role>();

            roleRepository.Add(role);
            await _uow.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            var roleRepository = _uow.GetRepository<Role>();

            roleRepository.Update(role);
            await _uow.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            var roleRepository = _uow.GetRepository<Role>();

            roleRepository.Remove(role);
            await _uow.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            var roleRepository = _uow.GetRepository<Role>();

            var role = await roleRepository.GetByIdAsync(roleId);

            return role;
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            var roleRepository = _uow.GetRepository<Role>();

            var role = await roleRepository.GetSingleOrDefaultAsync(r => r.NormalizedName == normalizedRoleName);

            return role;
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
