namespace CineMagic.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Common.Models;

    using static CineMagic.Data.Common.DataValidation.Movie;

    public class Movie : BaseDeletableModel<int>
    {
        public Movie()
        {
            this.Backdrops = new HashSet<MovieBackdrop>();
            this.Genres = new HashSet<MovieGenre>();
            this.Cast = new HashSet<MovieActor>();
            this.ProductionCountries = new HashSet<MovieCountry>();
            this.Ratings = new HashSet<Rating>();
            this.Watchlists = new HashSet<Watchlist>();
            this.Reviews = new HashSet<Review>();
            this.Comments = new HashSet<MovieComment>();
        }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(PosterPathMaxLength)]
        public string PosterPath { get; set; }

        [MaxLength(TrailerPathMaxLength)]
        public string TrailerPath { get; set; }

        [Required]
        [MaxLength(IMDBLinkMaxLength)]
        public string IMDBLink { get; set; }

        public DateTime ReleaseDate { get; set; }

        [MaxLength(RuntimeMaxLength)]
        public int Runtime { get; set; }

        [MaxLength(TaglineMaxLength)]
        public string Tagline { get; set; }

        [Required]
        [MaxLength(OverviewMaxLength)]
        public string Overview { get; set; }

        [Required]
        [MaxLength(LanguageMaxLength)]
        public string Language { get; set; }

        public double Budget { get; set; }

        public double Revenue { get; set; }

        public double Popularity { get; set; }

        public double CurrentAverageVote { get; set; }

        public int CurrentNumberOfVotes { get; set; }

        public int DirectorId { get; set; }

        public virtual Director Director { get; set; }

        public virtual ICollection<MovieBackdrop> Backdrops { get; set; }

        public virtual ICollection<MovieGenre> Genres { get; set; }

        public virtual ICollection<MovieActor> Cast { get; set; }

        public virtual ICollection<MovieCountry> ProductionCountries { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Watchlist> Watchlists { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<MovieComment> Comments { get; set; }
    }
}
