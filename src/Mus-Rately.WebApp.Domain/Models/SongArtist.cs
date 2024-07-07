namespace Mus_Rately.WebApp.Domain.Models
{
    public class SongArtist
    {
        public int ArtistId { get; set; }

        public int SongId { get; set; }

        public Artist Artist { get; set; }

        public Song Song { get; set; }
    }
}
