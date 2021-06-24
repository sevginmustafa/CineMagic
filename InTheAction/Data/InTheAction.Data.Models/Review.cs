namespace InTheAction.Data.Models
{
    using System;

    using InTheAction.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
