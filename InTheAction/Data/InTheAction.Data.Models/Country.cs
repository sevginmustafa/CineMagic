namespace InTheAction.Data.Models
{
    using System.Collections.Generic;

    using InTheAction.Data.Common.Models;

    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            this.Movies = new HashSet<MovieCountry>();
        }

        public string Name { get; set; }

        public virtual ICollection<MovieCountry> Movies { get; set; }
    }
}
