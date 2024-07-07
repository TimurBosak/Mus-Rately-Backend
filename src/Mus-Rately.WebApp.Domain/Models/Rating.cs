namespace Mus_Rately.WebApp.Domain.Models
{
    public class Rating
    {
        public int Id { get; set; }

        public int RhymesMark { get; set; }

        public int StructureMark { get; set; }

        public int RealisationMark { get; set; }

        public int CharismaMark { get; set; }

        public int AtmosphereMark { get; set; }

        public int PopularityMark { get; set; }

        public int TotalMark { get; set; }
    }
}
