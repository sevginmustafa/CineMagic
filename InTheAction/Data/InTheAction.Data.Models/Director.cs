namespace InTheAction.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InTheAction.Data.Common.Models;
    using InTheAction.Data.Models.Enums;

    public class Director : BaseDeletableModel<int>
    {
        public Director()
        {
            this.Movies = new HashSet<Movie>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CoverImageUrl { get; set; }

        [Required]
        public string Biography { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime? Deathday { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
