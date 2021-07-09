namespace InTheAction.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InTheAction.Data.Common.Models;
    using InTheAction.Data.Models.Enums;

    using static InTheAction.Data.Common.DataValidation.Person;

    public class Director : BaseDeletableModel<int>
    {
        public Director()
        {
            this.Movies = new HashSet<Movie>();
            this.Comments = new HashSet<DirectorComment>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ProfilePathlMaxLength)]
        public string ProfilePath { get; set; }

        public string Biography { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime? Deathday { get; set; }

        public string Birthplace { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

        public virtual ICollection<DirectorComment> Comments { get; set; }
    }
}
