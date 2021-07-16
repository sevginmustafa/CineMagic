namespace CineMagic.Data.Models
{
    using CineMagic.Data.Common.Models;

    public class MovieBackdrop : BaseDeletableModel<int>
    {
        public string Path { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
