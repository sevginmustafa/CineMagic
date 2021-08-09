namespace CineMagic.Web.ViewModels.Movies
{
    using System;
    using System.Linq;

    using AutoMapper;
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieStandartViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterPath { get; set; }

        public DateTime ReleaseDate { get; set; }

        public double Rating { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieStandartViewModel>()
                .ForMember(x => x.Rating, opt =>
                opt.MapFrom(x => x.Ratings.Count > 0 ? x.Ratings.Average(x => x.Rate) : 0));
        }
    }
}
