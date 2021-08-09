namespace CineMagic.Data.Models
{
    using System;

    using CineMagic.Data.Common.Models;

    public class AuthorReview : IDeletableEntity
    {
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public int ReviewId { get; set; }

        public virtual Review Review { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
