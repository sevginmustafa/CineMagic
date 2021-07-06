namespace InTheAction.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InTheAction.Data.Common.Models;

    public class Genre : BaseDeletableModel<int>
    {
        public Genre()
        {
            this.Movies = new HashSet<MovieGenre>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<MovieGenre> Movies { get; set; }
    }
}
