namespace InTheAction.Data.Models
{
    using System;
    using System.Collections.Generic;

    using InTheAction.Data.Common.Models;

    public class Producer : BaseDeletableModel<int>
    {
        public Producer()
        {
            this.Movies = new HashSet<MovieProducer>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Birthplace { get; set; }

        public virtual ICollection<MovieProducer> Movies { get; set; }
    }
}
