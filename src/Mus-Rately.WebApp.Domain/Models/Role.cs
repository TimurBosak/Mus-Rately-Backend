using Microsoft.AspNet.Identity.EntityFramework;

namespace Mus_Rately.WebApp.Domain.Models
{
    public class Role : IdentityRole
    {
        public const string User = "User";
        public const string Artist = "Artist";
        public const string SiteOwner = "SiteOwner";
        public const string Admin = "Admin";


        public ICollection<UserRole> Users { get; set; }
    }
}
