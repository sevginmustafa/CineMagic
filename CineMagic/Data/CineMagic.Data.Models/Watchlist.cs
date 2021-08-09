namespace CineMagic.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CineMagic.Data.Common.Models;

    public class Watchlist : IAuditInfo
    {
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
