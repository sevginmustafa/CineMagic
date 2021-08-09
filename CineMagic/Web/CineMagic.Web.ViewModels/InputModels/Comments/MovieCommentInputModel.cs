namespace CineMagic.Web.ViewModels.InputModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using static CineMagic.Data.Common.DataValidation.Comment;

    public class MovieCommentInputModel
    {
        public int MovieId { get; set; }

        [Required]
        public string UserId { get; set; }

        public int? ParentId { get; set; }

        [Required]
        [MinLength(ContentMinLength, ErrorMessage = "Content should be more than 3 characters long!")]
        [MaxLength(ContentMaxLength, ErrorMessage = "Content should be less than 1000 characters long!")]
        public string Content { get; set; }
    }
}
