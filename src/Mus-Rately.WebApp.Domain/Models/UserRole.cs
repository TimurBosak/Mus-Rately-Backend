using Microsoft.AspNetCore.Identity;

namespace Mus_Rately.WebApp.Domain.Models
{
    public class UserRole : IdentityUserRole<string>
    {
        public User User { get; set; }

        public Role Role { get; set; }
    }
}
