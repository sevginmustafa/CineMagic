namespace CineMagic.Web.ViewModels.Directors
{
    using AutoMapper;
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class DirectorStandartViewModel : IMapFrom<Director>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string ProfilePicPath { get; set; }

        public double Popularity { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Director, DirectorStandartViewModel>()
              .ForMember(x => x.ProfilePicPath, opt =>
              opt.MapFrom(x => x.ProfilePicPath ?? "/images/no-profile-pic.jpg"));
        }
    }
}
