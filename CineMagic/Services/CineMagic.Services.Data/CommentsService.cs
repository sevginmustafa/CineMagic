namespace CineMagic.Services.Data
{
    using System.Threading.Tasks;

    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels.InputModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly IRepository<MovieComment> movieCommentsRepository;
        private readonly IRepository<ActorComment> actorCommentsRepository;
        private readonly IRepository<DirectorComment> directorCommentsRepository;

        public CommentsService(
            IRepository<MovieComment> movieCommentsRepository,
            IRepository<ActorComment> actorCommentsRepository,
            IRepository<DirectorComment> directorCommentsRepository)
        {
            this.movieCommentsRepository = movieCommentsRepository;
            this.actorCommentsRepository = actorCommentsRepository;
            this.directorCommentsRepository = directorCommentsRepository;
        }

        public async Task CreateMovieCommentAsync(MovieCommentInputModel inputModel)
        {
            var parentId = inputModel.ParentId != 0 ? inputModel.ParentId : null;

            var comment = new MovieComment
            {
                MovieId = inputModel.MovieId,
                UserId = inputModel.UserId,
                ParentId = parentId,
                Content = inputModel.Content,
            };

            await this.movieCommentsRepository.AddAsync(comment);
            await this.movieCommentsRepository.SaveChangesAsync();
        }

        public async Task CreateActorCommentAsync(ActorCommentInputModel inputModel)
        {
            var parentId = inputModel.ParentId != 0 ? inputModel.ParentId : null;

            var comment = new ActorComment
            {
                ActorId = inputModel.ActorId,
                UserId = inputModel.UserId,
                ParentId = parentId,
                Content = inputModel.Content,
            };

            await this.actorCommentsRepository.AddAsync(comment);
            await this.actorCommentsRepository.SaveChangesAsync();
        }

        public async Task CreateDirectorCommentAsync(DirectorCommentInputModel inputModel)
        {
            var parentId = inputModel.ParentId != 0 ? inputModel.ParentId : null;

            var comment = new DirectorComment
            {
                DirectorId = inputModel.DirectorId,
                UserId = inputModel.UserId,
                ParentId = parentId,
                Content = inputModel.Content,
            };

            await this.directorCommentsRepository.AddAsync(comment);
            await this.directorCommentsRepository.SaveChangesAsync();
        }
    }
}
