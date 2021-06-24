namespace InTheAction.Data.Models
{
    using System;
    using System.Collections.Generic;

    using InTheAction.Data.Common.Models;

    public class Director : BaseDeletableModel<int>
    {
        public Director()
        {
            this.Movies = new HashSet<MovieDirector>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Birthplace { get; set; }

        public virtual ICollection<MovieDirector> Movies { get; set; }
    }
}
