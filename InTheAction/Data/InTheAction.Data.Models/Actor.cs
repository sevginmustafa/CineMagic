namespace InTheAction.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InTheAction.Data.Common.Models;
    using InTheAction.Data.Models.Enums;

    using static InTheAction.Data.Common.DataValidation.Person;

    public class Actor : BaseDeletableModel<int>
    {
        public Actor()
        {
            this.Movies = new HashSet<MovieActor>();
            this.Comments = new HashSet<ActorComment>();
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

        public virtual ICollection<MovieActor> Movies { get; set; }

        public virtual ICollection<ActorComment> Comments { get; set; }
    }
}
