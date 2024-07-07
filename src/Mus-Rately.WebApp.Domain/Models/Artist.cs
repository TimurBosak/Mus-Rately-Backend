namespace Mus_Rately.WebApp.Domain.Models
{
    public class Artist
    {
        public const int MaxLength = 100;


        public int Id { get; set; }

        public string Name { get; set; }

        public decimal UserRating { get; set; }

        public decimal SiteOwnerRating { get; set; }

        public string ArtistImage { get; set; }
    }
}
