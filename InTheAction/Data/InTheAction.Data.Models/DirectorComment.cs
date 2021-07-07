namespace InTheAction.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using InTheAction.Data.Common.Models;

    using static InTheAction.Data.Common.DataValidation.Comment;

    public class DirectorComment : BaseModel<int>
    {
        public int DirectorId { get; set; }

        public virtual Director Director { get; set; }

        public int? ParentId { get; set; }

        public virtual DirectorComment Parent { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
