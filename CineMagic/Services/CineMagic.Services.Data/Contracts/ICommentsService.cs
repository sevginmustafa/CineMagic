namespace CineMagic.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using CineMagic.Web.ViewModels.InputModels.Comments;

    public interface ICommentsService
    {
        Task CreateMovieCommentAsync(MovieCommentInputModel inputModel);

        Task CreateActorCommentAsync(ActorCommentInputModel inputModel);

        Task CreateDirectorCommentAsync(DirectorCommentInputModel inputModel);
    }
}
