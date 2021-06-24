namespace InTheAction.Data.Models
{
    using InTheAction.Data.Common.Models;

    public class MovieActor : BaseDeletableModel<int>
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int ActorId { get; set; }

        public virtual Actor Actor { get; set; }
    }
}
