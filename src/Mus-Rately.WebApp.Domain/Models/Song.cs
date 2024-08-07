﻿namespace Mus_Rately.WebApp.Domain.Models
{
    public class Song
    {
        public const int MaxLength = 100;


        public int Id { get; set; }

        public string Name { get; set; }

        public decimal UserRating { get; set; }

        public decimal AuthorRating { get; set; }

        public string TrackImage { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
