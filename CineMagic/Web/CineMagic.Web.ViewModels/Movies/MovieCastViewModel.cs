namespace CineMagic.Web.ViewModels.Movies
{
    using AutoMapper;
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieCastViewModel : IMapFrom<MovieActor>, IHaveCustomMappings
    {
        public int ActorId { get; set; }

        public string ActorName { get; set; }

        public string CharacterName { get; set; }

        public string ActorProfilePicPath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MovieActor, MovieCastViewModel>()
                .ForMember(x => x.ActorProfilePicPath, opt =>
                opt.MapFrom(x => x.Actor.ProfilePicPath ?? "/images/no-profile-pic.jpg"));
        }
    }
}
