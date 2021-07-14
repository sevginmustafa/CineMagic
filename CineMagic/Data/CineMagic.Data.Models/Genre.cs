namespace CineMagic.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Common.Models;

    using static CineMagic.Data.Common.DataValidation.Genre;

    public class Genre : BaseDeletableModel<int>
    {
        public Genre()
        {
            this.Movies = new HashSet<MovieGenre>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<MovieGenre> Movies { get; set; }
    }
}
