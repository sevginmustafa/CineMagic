namespace InTheAction.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using InTheAction.Data.Common.Models;

    using static InTheAction.Data.Common.DataValidation.Movie;

    public class Movie : BaseDeletableModel<int>
    {
        public Movie()
        {
            this.Genres = new HashSet<MovieGenre>();
            this.Cast = new HashSet<MovieActor>();
            this.Ratings = new HashSet<Rating>();
            this.Reviews = new HashSet<Review>();
            this.Comments = new HashSet<MovieComment>();
        }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(CoverImageUrlMaxLength)]
        public string CoverImageUrl { get; set; }

        [Required]
        [MaxLength(TrailerUrlMaxLength)]
        public string TrailerUrl { get; set; }

        [Required]
        [MaxLength(TMDBLinkMaxLength)]
        public string TMDBLink { get; set; }

        public short ReleaseYear { get; set; }

        [Required]
        [MaxLength(RuntimeMaxLength)]
        public string Runtime { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(LanguageMaxLength)]
        public string Language { get; set; }

        public double Budget { get; set; }

        public double Revenue { get; set; }

        public int DirectorId { get; set; }

        public virtual Director Director { get; set; }

        public virtual ICollection<MovieGenre> Genres { get; set; }

        public virtual ICollection<MovieActor> Cast { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<MovieComment> Comments { get; set; }
    }
}
