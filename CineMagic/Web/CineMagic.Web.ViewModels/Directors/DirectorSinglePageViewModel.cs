namespace CineMagic.Web.ViewModels.Directors
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class DirectorSinglePageViewModel : IMapFrom<Director>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProfilePicPath { get; set; }

        public string Biography { get; set; }

        public string Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public int? Age => this.Birthday != null ? (DateTime.Today - this.Birthday.Value).Days / 365 : null;

        public int KnownCredits => this.Movies.Count;

        public DateTime? Deathday { get; set; }

        public string Birthplace { get; set; }

        public double Popularity { get; set; }

        public virtual ICollection<DirectorMoviesViewModel> Movies { get; set; }

        //public virtual ICollection<DirectorComment> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Director, DirectorSinglePageViewModel>().
                ForMember(x => x.ProfilePicPath, opt => opt.MapFrom(x => x.ProfilePicPath ?? "/images/no-profile-pic.jpg"));
        }
    }
}
