namespace InTheAction.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using InTheAction.Data.Common.Models;

    public class Movie : BaseDeletableModel<int>
    {
        public Movie()
        {
            this.Rating = 0;
            this.NumberOfVotes = 0;
            this.Genres = new HashSet<MovieGenre>();
            this.Cast = new HashSet<MovieActor>();
            this.Reviews = new HashSet<Review>();
            this.News = new HashSet<News>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string CoverImageUrl { get; set; }

        [Required]
        public string TrailerUrl { get; set; }

        public short ReleaseYear { get; set; }

        [Required]
        public string Runtime { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Language { get; set; }

        public double Budget { get; set; }

        public double Revenue { get; set; }

        public double Rating { get; set; }

        public int NumberOfVotes { get; set; }

        public int DirectorId { get; set; }

        public virtual Director Director { get; set; }

        public virtual ICollection<MovieGenre> Genres { get; set; }

        public virtual ICollection<MovieActor> Cast { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
