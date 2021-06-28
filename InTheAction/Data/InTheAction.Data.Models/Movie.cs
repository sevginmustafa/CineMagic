namespace InTheAction.Data.Models
{
    using System.Collections.Generic;

    using InTheAction.Data.Common.Models;

    public class Movie : BaseDeletableModel<int>
    {
        public Movie()
        {
            this.Genres = new HashSet<MovieGenre>();
            this.Cast = new HashSet<MovieActor>();
            this.Directors = new HashSet<MovieDirector>();
            this.Producers = new HashSet<MovieProducer>();
            this.Countries = new HashSet<MovieCountry>();
            this.Reviews = new HashSet<Review>();
            this.News = new HashSet<News>();
        }

        public string Title { get; set; }

        public string CoverImageUrl { get; set; }

        public string TrailerUrl { get; set; }

        public short ReleaseYear { get; set; }

        public string Runtime { get; set; }

        public string Description { get; set; }

        public double Rating { get; set; }

        public int NumberOfVotes { get; set; }

        public string Language { get; set; }

        public double Budget { get; set; }

        public virtual ICollection<MovieGenre> Genres { get; set; }

        public virtual ICollection<MovieActor> Cast { get; set; }

        public virtual ICollection<MovieDirector> Directors { get; set; }

        public virtual ICollection<MovieProducer> Producers { get; set; }

        public virtual ICollection<MovieCountry> Countries { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
