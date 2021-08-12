namespace CineMagic.Web.ViewModels.InputModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using static CineMagic.Common.ModelValidation.Comment;

    public class ActorCommentInputModel
    {
        public int ActorId { get; set; }

        [Required]
        public string UserId { get; set; }

        public int? ParentId { get; set; }

        [Required]
        [StringLength(ContentMaxLength, ErrorMessage = ContentErrorMessage, MinimumLength = ContentMinLength)]
        public string Content { get; set; }
    }
}
