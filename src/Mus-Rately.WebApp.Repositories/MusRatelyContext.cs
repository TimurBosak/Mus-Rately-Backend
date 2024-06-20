using Microsoft.EntityFrameworkCore;

namespace Mus_Rately.WebApp.Repositories
{
    public class MusRatelyContext : DbContext
    {
        public MusRatelyContext(DbContextOptions<MusRatelyContext> options)
            : base(options)
        {

        }
    }
}
