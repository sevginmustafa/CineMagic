namespace CineMagic.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieRespTabsViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterPath { get; set; }

        public string TrailerPath { get; set; }

        public DateTime ReleaseDate { get; set; }

        public double CurrentAverageVote { get; set; }

        public string Overview { get; set; }

        public ICollection<MovieGenresViewModel> Genres { get; set; }
    }
}
