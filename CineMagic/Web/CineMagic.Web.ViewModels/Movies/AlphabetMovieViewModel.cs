namespace CineMagic.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;

    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class AlphabetMovieViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterPath { get; set; }

        public ICollection<MovieGenresViewModel> Genres { get; set; }

        public ICollection<MovieCountriesViewModel> ProductionCountries { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Runtime { get; set; }

        public string Language { get; set; }

        public double Budget { get; set; }

        public double Revenue { get; set; }

        public double Popularity { get; set; }

        public double CurrentAverageVote { get; set; }

        public int CurrentNumberOfVotes { get; set; }

        public string DirectorName { get; set; }
    }
}
