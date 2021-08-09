namespace CineMagic.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Common.Models;

    using static CineMagic.Data.Common.DataValidation.Review;

    public class Review : BaseModel<int>
    {
        public Review()
        {
            this.MovieReviews = new HashSet<MovieReview>();
            this.Authors = new HashSet<AuthorReview>();
        }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<MovieReview> MovieReviews { get; set; }

        public virtual ICollection<AuthorReview> Authors { get; set; }
    }
}
