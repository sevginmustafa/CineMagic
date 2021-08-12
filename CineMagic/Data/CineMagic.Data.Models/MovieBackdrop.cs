namespace CineMagic.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Common.Models;

    using static CineMagic.Data.Common.DataValidation.Backdrop;

    public class MovieBackdrop : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(BackdropPathMaxLength)]
        public string Path { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
