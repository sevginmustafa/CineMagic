namespace InTheAction.Data.Models
{
    using InTheAction.Data.Common.Models;

    public class MovieDirector : BaseDeletableModel<int>
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int DirectorId { get; set; }

        public virtual Director Director { get; set; }
    }
}
