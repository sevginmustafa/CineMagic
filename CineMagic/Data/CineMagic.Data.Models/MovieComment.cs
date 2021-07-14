namespace CineMagic.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Common.Models;

    using static CineMagic.Data.Common.DataValidation.Comment;

    public class MovieComment : BaseModel<int>
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int? ParentId { get; set; }

        public virtual MovieComment Parent { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
