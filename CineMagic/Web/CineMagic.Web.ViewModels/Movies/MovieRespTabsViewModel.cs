namespace CineMagic.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieRespTabsViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterPath { get; set; }

        public string TrailerPath { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Overview { get; set; }

        public double Rating { get; set; }

        public ICollection<MovieGenresViewModel> Genres { get; set; }

        public ICollection<MovieLanguagesViewModel> Languages { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieRespTabsViewModel>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Ratings.Count > 0 ? x.Ratings.Average(x => x.Rate) : 0));
        }
    }
}
