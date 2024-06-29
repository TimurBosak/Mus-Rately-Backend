using Microsoft.EntityFrameworkCore;
using Mus_Rately.WebApp.Domain.Models;

namespace Mus_Rately.WebApp.Repositories
{
    public class MusRatelyContext : DbContext
    {
        public MusRatelyContext(DbContextOptions<MusRatelyContext> options)
            : base(options)
        {

        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>()
                .Property(s => s.Name)
                .HasMaxLength(Song.MaxLength)
                .IsRequired();
        }
    }
}
