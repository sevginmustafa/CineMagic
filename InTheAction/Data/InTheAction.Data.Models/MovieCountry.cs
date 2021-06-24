﻿namespace InTheAction.Data.Models
{
    using InTheAction.Data.Common.Models;

    public class MovieCountry : BaseDeletableModel<int>
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}