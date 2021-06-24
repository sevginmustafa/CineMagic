namespace InTheAction.Data.Models
{
    using InTheAction.Data.Common.Models;

    public class MovieProducer : BaseDeletableModel<int>
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int ProducerId { get; set; }

        public virtual Producer Producer { get; set; }
    }
}
