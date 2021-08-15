namespace CineMagic.Web.ViewModels.Movies
{
    using System.Linq;

    using AutoMapper;
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieFooterViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string BackdropPath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieFooterViewModel>()
                .ForMember(x => x.BackdropPath, opt => opt.MapFrom(x => x.Backdrops.Count > 0 ? x.Backdrops.FirstOrDefault().Path : x.PosterPath));
        }
    }
}
