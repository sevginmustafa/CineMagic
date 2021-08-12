namespace CineMagic.Web.ViewModels.Actors
{
    using System;

    using AutoMapper;
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class ActorAdministrationViewModel : IMapFrom<Actor>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProfilePicPath { get; set; }

        public DateTime? Birthday { get; set; }

        public int? Age => this.Birthday != null ? (DateTime.Today - this.Birthday.Value).Days / 365 : null;

        public DateTime? Deathday { get; set; }

        public string Birthplace { get; set; }

        public double Popularity { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Actor, ActorAdministrationViewModel>()
                .ForMember(x => x.ProfilePicPath, opt =>
                opt.MapFrom(x => x.ProfilePicPath ?? "/images/no-profile-pic.jpg"));
        }
    }
}
