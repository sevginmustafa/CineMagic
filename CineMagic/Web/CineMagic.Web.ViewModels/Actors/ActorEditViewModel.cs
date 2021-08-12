namespace CineMagic.Web.ViewModels.Actors
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Models;
    using CineMagic.Data.Models.Enums;
    using CineMagic.Services.Mapping;
    using static CineMagic.Common.ModelValidation.Person;

    public class ActorEditViewModel:IMapFrom<Actor>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, ErrorMessage = NameErrorMessage, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [StringLength(ProfilePicPathMaxLength, ErrorMessage = ProfilePicPathErrorMessage, MinimumLength = ProfilePicPathMinLength)]
        [Display(Name = ProfilePicPathDisplayName)]
        public string ProfilePicPath { get; set; }

        [Required]
        [StringLength(BiographyMaxLength, ErrorMessage = BiographyErrorMessage, MinimumLength = BiographyMinLength)]
        public string Biography { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Deathday { get; set; }

        [StringLength(BirthplaceMaxLength, ErrorMessage = BirthplaceErrorMessage, MinimumLength = BirthplaceMinLength)]
        public string Birthplace { get; set; }

        [Range(0, PopularityMaxValue)]
        public double Popularity { get; set; }
    }
}
