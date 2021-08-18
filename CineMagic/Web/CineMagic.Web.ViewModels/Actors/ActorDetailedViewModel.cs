namespace CineMagic.Web.ViewModels.Actors
{
    using System;

    using AutoMapper;
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class ActorDetailedViewModel : IMapFrom<Actor>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProfilePicPath { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime? Deathday { get; set; }

        public int? Age
        {
            get
            {
                if (this.Deathday.HasValue && this.Birthday.HasValue)
                {
                    return (this.Deathday.Value - this.Birthday.Value).Days / 365;
                }
                else if (this.Birthday.HasValue)
                {
                    return (DateTime.Today - this.Birthday.Value).Days / 365;
                }

                return null;
            }
        }

        public string Birthplace { get; set; }

        public double Popularity { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Actor, ActorDetailedViewModel>()
                .ForMember(x => x.ProfilePicPath, opt =>
                opt.MapFrom(x => x.ProfilePicPath ?? "/images/no-profile-pic.jpg"));
        }
    }
}
