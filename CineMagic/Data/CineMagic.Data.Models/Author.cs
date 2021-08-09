namespace CineMagic.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Common.Models;

    using static CineMagic.Data.Common.DataValidation.Author;

    public class Author : BaseDeletableModel<int>
    {
        public Author()
        {
            this.Reviews = new HashSet<AuthorReview>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<AuthorReview> Reviews { get; set; }
    }
}
