namespace CineMagic.Data.Models
{
    using CineMagic.Data.Common.Models;

    public class Rating : BaseDeletableModel<int>
    {
        public int Rate { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
