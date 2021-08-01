namespace CineMagic.Web.ViewModels.Movies
{
    using System;

    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieWatchlistViewModel : IMapFrom<Watchlist>
    {
        public int MovieId { get; set; }

        public string MovieTitle { get; set; }

        public string MoviePosterPath { get; set; }

        public DateTime MovieReleaseDate { get; set; }

        public double MovieCurrentAverageVote { get; set; }
    }
}
