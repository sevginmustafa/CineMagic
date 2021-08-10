namespace CineMagic.Web.Controllers
{
    using System.Threading.Tasks;

    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels.InputModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateMovieComment(MovieCommentInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.commentsService.CreateMovieCommentAsync(inputModel);

            return this.RedirectToAction("Details", "Movies", new { id = inputModel.MovieId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateActorComment(ActorCommentInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.commentsService.CreateActorCommentAsync(inputModel);

            return this.RedirectToAction("Details", "Actors", new { id = inputModel.ActorId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateDirectorComment(DirectorCommentInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.commentsService.CreateDirectorCommentAsync(inputModel);

            return this.RedirectToAction("Details", "Directors", new { id = inputModel.DirectorId });
        }
    }
}
