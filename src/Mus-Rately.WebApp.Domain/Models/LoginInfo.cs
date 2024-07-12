using Microsoft.AspNetCore.Identity;

namespace Mus_Rately.WebApp.Domain.Models
{
    public class LoginInfo : UserLoginInfo
    {
        public LoginInfo()
        : base(null, null, null)
        {
        }

        public string UserId { get; set; }
    }
}
