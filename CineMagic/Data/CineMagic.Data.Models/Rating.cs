namespace CineMagic.Data.Models
{
    using CineMagic.Data.Common.Models;

    public class Rating : BaseModel<int>
    {
        public int Rate { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
