namespace CineMagic.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Common.Models;

    using static CineMagic.Data.Common.DataValidation.Country;

    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            this.Movies = new HashSet<MovieCountry>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<MovieCountry> Movies { get; set; }
    }
}
