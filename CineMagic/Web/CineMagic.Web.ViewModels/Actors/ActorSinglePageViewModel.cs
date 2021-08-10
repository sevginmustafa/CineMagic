namespace CineMagic.Web.ViewModels.Actors
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;
    using CineMagic.Web.ViewModels.Comments;

    public class ActorSinglePageViewModel : IMapFrom<Actor>, IHaveCustomMappings
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

        public virtual ICollection<ActorMoviesViewModel> Movies { get; set; }

        public virtual ICollection<ActorCommentsViewModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Actor, ActorSinglePageViewModel>()
                .ForMember(x => x.ProfilePicPath, opt =>
                opt.MapFrom(x => x.ProfilePicPath ?? "/images/no-profile-pic.jpg"));
        }
    }
}
