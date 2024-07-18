using Microsoft.EntityFrameworkCore;
using Mus_Rately.Repositories.Implementations;
using Mus_Rately.WebApp.Domain.Models;

namespace Mus_Rately.WebApp.Repositories
{
    public class UserRoleRepository : Repository<UserRole>
    {
        public UserRoleRepository(DbContext context)
            : base(context)
        {

        }


        protected override IQueryable<UserRole> GetQuery()
        {
            return base.GetQuery().Include(ur => ur.Role);
        }
    }
}
