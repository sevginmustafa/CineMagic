namespace CineMagic.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieSinglePageViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterPath { get; set; }

        public string TrailerPath { get; set; }

        public string IMDBLink { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Runtime { get; set; }

        public string Tagline { get; set; }

        public string Overview { get; set; }

        public string Language { get; set; }

        public double Budget { get; set; }

        public double Revenue { get; set; }

        public double Popularity { get; set; }

        public double CurrentAverageVote { get; set; }

        public int CurrentNumberOfVotes { get; set; }

        public double Rating { get; set; }

        public string DirectorName { get; set; }

        public ICollection<MovieGenresViewModel> Genres { get; set; }

        public virtual ICollection<MovieCastViewModel> Cast { get; set; }

        public ICollection<MovieCountriesViewModel> ProductionCountries { get; set; }

        //public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<string> WatchlistUsers { get; set; }

        //public virtual ICollection<Review> Reviews { get; set; }

        //public virtual ICollection<MovieComment> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieSinglePageViewModel>()
                .ForMember(x => x.WatchlistUsers, opt => opt.MapFrom(x => x.Watchlists.Select(x => x.UserId)))
                .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Ratings.Count > 0 ? x.Ratings.Average(x => x.Rate) : 0));
        }
    }
}
