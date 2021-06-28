namespace InTheAction.Data.Models
{
    using System;

    using InTheAction.Data.Common.Models;

    public class News : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string SubDescription { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
