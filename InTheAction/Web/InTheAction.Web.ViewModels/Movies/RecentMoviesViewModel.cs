namespace InTheAction.Web.ViewModels.Movies
{
    using System;

    using InTheAction.Data.Models;
    using InTheAction.Services.Mapping;

    public class RecentMoviesViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PosterPath { get; set; }

        public string TrailerPath { get; set; }

        public DateTime ReleaseDate { get; set; }

        public double CurrentAverageVote { get; set; }

        public int CurrentNumberOfVotes { get; set; }
    }
}
