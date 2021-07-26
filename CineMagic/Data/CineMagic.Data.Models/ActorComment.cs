namespace CineMagic.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Common.Models;

    using static CineMagic.Data.Common.DataValidation.Comment;

    public class ActorComment : BaseModel<int>
    {
        public int ActorId { get; set; }

        public virtual Actor Actor { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int? ParentId { get; set; }

        public virtual ActorComment Parent { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }
    }
}
