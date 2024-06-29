using Mus_Rately.Repositories.Implementations;
using Mus_Rately.WebApp.Repositories.Interfaces;

namespace Mus_Rately.WebApp.Repositories
{
    public class MusRatelyUnitOfWork : UnitOfWork<MusRatelyContext>, IMusRatelyUnitOfWork
    {
        public MusRatelyUnitOfWork(MusRatelyContext context) 
            : base(context)
        {
        }
    }
}
