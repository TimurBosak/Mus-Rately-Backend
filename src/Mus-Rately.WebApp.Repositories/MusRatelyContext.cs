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

            modelBuilder.Entity<Rating>();

            modelBuilder.Entity<UserSongRating>()
                .HasKey(usr => new { usr.UserId, usr.SongId, usr.RatingId });
            modelBuilder.Entity<UserSongRating>()
                .HasIndex(usr => usr.UserId);
            modelBuilder.Entity<UserSongRating>()
                .HasIndex(usr => usr.SongId);
            modelBuilder.Entity<UserSongRating>()
                .HasIndex(usr => usr.RatingId);

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(User.MaxLength)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.RoleId, ur.UserId });
            modelBuilder.Entity<UserRole>()
                .HasIndex(ur => ur.RoleId);
            modelBuilder.Entity<UserRole>()
                .HasIndex(ur => ur.UserId);

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = "User",
                    ConcurrencyStamp = "userConcurrencyStamp",
                    Name = Role.User,
                    NormalizedName = Role.User.ToUpper()
                },
                new Role
                {
                    Id = "Artist",
                    ConcurrencyStamp = "artistConcurrencyStamp",
                    Name = Role.Artist,
                    NormalizedName = Role.Artist.ToUpper()
                },
                new Role
                {
                    Id = "SiteOwner",
                    ConcurrencyStamp = "siteownerConcurrencyStamp",
                    Name = Role.SiteOwner,
                    NormalizedName = Role.SiteOwner.ToUpper()
                },
                new Role
                {
                    Id = "Admin",
                    ConcurrencyStamp = "adminConcurrencyStamp",
                    Name = Role.Admin,
                    NormalizedName = Role.Admin.ToUpper()
                });
        }
    }
}
