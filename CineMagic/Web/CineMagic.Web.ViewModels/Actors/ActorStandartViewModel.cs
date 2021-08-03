namespace CineMagic.Web.ViewModels.Actors
{
    using AutoMapper;
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class ActorStandartViewModel : IMapFrom<Actor>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string ProfilePicPath { get; set; }

        public double Popularity { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Actor, ActorStandartViewModel>()
                .ForMember(x => x.ProfilePicPath, opt =>
                opt.MapFrom(x => x.ProfilePicPath ?? "/images/no-profile-pic.jpg"));
        }
    }
}
