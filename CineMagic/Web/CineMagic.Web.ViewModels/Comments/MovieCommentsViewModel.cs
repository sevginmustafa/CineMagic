namespace CineMagic.Web.ViewModels.Comments
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieCommentsViewModel : BaseCommentViewModel, IMapFrom<MovieComment>
    {
    }
}
