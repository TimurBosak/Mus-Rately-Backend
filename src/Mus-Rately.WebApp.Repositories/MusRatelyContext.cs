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
            modelBuilder.Entity<Song>()
                .Property(s => s.UserRating)
                .HasColumnType("decimal(4,2)")
                .HasPrecision(4, 2);
            modelBuilder.Entity<Song>()
                .Property(s => s.AuthorRating)
                .HasColumnType("decimal(4,2)")
                .HasPrecision(4, 2);

            modelBuilder.Entity<Artist>()
                .Property(a => a.Name)
                .HasMaxLength(Artist.MaxLength)
                .IsRequired();
            modelBuilder.Entity<Artist>()
                .Property(a => a.UserRating)
                .HasColumnType("decimal(4,2)")
                .HasPrecision(4, 2);
            modelBuilder.Entity<Artist>()
                .Property(a => a.SiteOwnerRating)
                .HasColumnType("decimal(4,2)")
                .HasPrecision(4, 2);

            modelBuilder.Entity<SongArtist>()
                .HasKey(sa => new { sa.SongId, sa.ArtistId });
            modelBuilder.Entity<SongArtist>()
                .HasIndex(sa => sa.SongId);
            modelBuilder.Entity<SongArtist>()
                .HasIndex(sa => sa.ArtistId);
        }
    }
}
