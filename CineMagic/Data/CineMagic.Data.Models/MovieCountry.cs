namespace CineMagic.Data.Models
{
    using CineMagic.Data.Common.Models;

    public class MovieCountry : BaseDeletableModel<int>
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual Country Country { get; set; }

        public int CountryId { get; set; }
    }
}
