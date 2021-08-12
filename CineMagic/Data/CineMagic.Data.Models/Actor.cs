namespace CineMagic.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Common.Models;
    using CineMagic.Data.Models.Enums;

    using static CineMagic.Data.Common.DataValidation.Person;

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

        [MaxLength(ProfilePicPathMaxLength)]
        public string ProfilePicPath { get; set; }

        [Required]
        [MaxLength(BiographyMaxLength)]
        public string Biography { get; set; }

        public Gender Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime? Deathday { get; set; }

        [MaxLength(BirthplaceMaxLength)]
        public string Birthplace { get; set; }

        public double Popularity { get; set; }

        public virtual ICollection<MovieActor> Movies { get; set; }

        public virtual ICollection<ActorComment> Comments { get; set; }
    }
}
