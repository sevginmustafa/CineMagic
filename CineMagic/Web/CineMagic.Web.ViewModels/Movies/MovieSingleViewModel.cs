namespace CineMagic.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieSingleViewModel : IMapFrom<Movie>
    {
        public string Title { get; set; }

        public string PosterPath { get; set; }

        public string TrailerPath { get; set; }

        public string IMDBLink { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Runtime { get; set; }

        public string Overview { get; set; }

        public string Language { get; set; }

        public double Budget { get; set; }

        public double Revenue { get; set; }

        public double Popularity { get; set; }

        public double CurrentAverageVote { get; set; }

        public int CurrentNumberOfVotes { get; set; }

        //public int DirectorId { get; set; }

        //public virtual Director Director { get; set; }

        //public virtual ICollection<MovieBackdrop> Backdrops { get; set; }

        //public virtual ICollection<MovieGenre> Genres { get; set; }

        //public virtual ICollection<MovieActor> Cast { get; set; }

        //public virtual ICollection<MovieCountry> ProductionCountries { get; set; }

        //public virtual ICollection<Rating> Ratings { get; set; }

        //public virtual ICollection<Watchlist> Watchlists { get; set; }

        //public virtual ICollection<Review> Reviews { get; set; }

        //public virtual ICollection<MovieComment> Comments { get; set; }
    }
}
