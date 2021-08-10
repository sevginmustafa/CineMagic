namespace CineMagic.Web.ViewModels.Comments
{
    using System;

    public abstract class BaseCommentViewModel
    {
        public int Id { get; set; }

        public string UserUsername { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
