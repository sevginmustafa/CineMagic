namespace CineMagic.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Common.Models;

    using static CineMagic.Data.Common.DataValidation.Review;

    public class Review : BaseModel<int>
    {
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
