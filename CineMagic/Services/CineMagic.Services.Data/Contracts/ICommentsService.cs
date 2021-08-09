namespace CineMagic.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using CineMagic.Web.ViewModels.InputModels.Comments;

    public interface ICommentsService
    {
        Task CreateMovieComment(MovieCommentInputModel inputModel);
    }
}
