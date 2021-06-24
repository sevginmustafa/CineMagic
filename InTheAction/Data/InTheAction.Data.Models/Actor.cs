namespace InTheAction.Data.Models
{
    using System;
    using System.Collections.Generic;

    using InTheAction.Data.Common.Models;

    public class Actor : BaseDeletableModel<int>
    {
        public Actor()
        {
            this.Movies = new HashSet<MovieActor>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Birthplace { get; set; }

        public virtual ICollection<MovieActor> Movies { get; set; }
    }
}
