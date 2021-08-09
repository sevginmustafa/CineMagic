namespace CineMagic.Web.ViewModels.Movies
{
    using System;

    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieCommentsViewModel : IMapFrom<MovieComment>
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public MovieCommentUserViewModel User { get; set; }

        public MovieCommentsViewModel Parent { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
