namespace CineMagic.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Common.Models;

    using static CineMagic.Data.Common.DataValidation.Language;

    public class Language : BaseDeletableModel<int>
    {
        public Language()
        {
            this.Movies = new HashSet<MovieLanguage>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<MovieLanguage> Movies { get; set; }
    }
}
