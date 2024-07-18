using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Mus_Rately.WebApp.Domain.Models;
using System.Security.Claims;

namespace Mus_Rately.WebApp.Services.Authentication
{
    public class MusRatelyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public MusRatelyUserClaimsPrincipalFactory(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IOptions<IdentityOptions> options) 
            : base(userManager, roleManager, options)
        {

        }


        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("DisplayName", user.Name));

            return identity;
        }
    }
}
