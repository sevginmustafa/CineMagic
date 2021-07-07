namespace InTheAction.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using InTheAction.Data.Common.Models;

    using static InTheAction.Data.Common.DataValidation.Comment;

    public class ActorComment : BaseModel<int>
    {
        public int ActorId { get; set; }

        public virtual Actor Actor { get; set; }

        public int? ParentId { get; set; }

        public virtual ActorComment Parent { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
