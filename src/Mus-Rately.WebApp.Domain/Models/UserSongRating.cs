namespace Mus_Rately.WebApp.Domain.Models
{
    public class UserSongRating
    {
        public const int MaxLength = 500;


        public string Review { get; set; }

        public int UserId { get; set; }

        public int SongId { get; set; }

        public int RatingId { get; set; }

        public User User { get; set; }

        public Song Song { get; set; }

        public Rating Rating { get; set; }
    }
}
