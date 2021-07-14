namespace CineMagic.Data.Models
{
    using CineMagic.Data.Common.Models;

    public class MovieGenre : BaseDeletableModel<int>
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
    }
}
