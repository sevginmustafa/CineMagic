﻿namespace InTheAction.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InTheAction.Data.Common.Models;

    public class City : BaseDeletableModel<int>
    {
        public City()
        {
            this.Actors = new HashSet<Actor>();
            this.Directors = new HashSet<Director>();
        }

        [Required]
        public string Name { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }

        public virtual ICollection<Director> Directors { get; set; }
    }
}