using Microsoft.AspNet.Identity.EntityFramework;

namespace Mus_Rately.WebApp.Domain.Models
{
    public class User : IdentityUser
    {
        public const int MaxLength = 100;

        public string Name { get; set; }

        public string UserImage { get; set; }

        public ICollection<UserRole> Roles { get; set; }
    }
}
