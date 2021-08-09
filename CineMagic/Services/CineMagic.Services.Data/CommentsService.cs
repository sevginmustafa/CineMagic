namespace CineMagic.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels.InputModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly IRepository<MovieComment> movieCommentsRepository;

        public CommentsService(IRepository<MovieComment> movieCommentsRepository)
        {
            this.movieCommentsRepository = movieCommentsRepository;
        }

        public async Task CreateMovieComment(MovieCommentInputModel inputModel)
        {
            var comment = new MovieComment
            {
                MovieId = inputModel.MovieId,
                UserId = inputModel.UserId,
                Content = inputModel.Content,
            };

            await this.movieCommentsRepository.AddAsync(comment);
            await this.movieCommentsRepository.SaveChangesAsync();
        }

        public async Task ReplyToMovieComment(MovieCommentInputModel inputModel)
        {
            var parent = this.movieCommentsRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.MovieId == inputModel.MovieId && x.UserId == inputModel.UserId);

            var reply = new MovieComment
            {
                MovieId = inputModel.MovieId,
                UserId = inputModel.UserId,
                Parent = parent,
                Content = inputModel.Content,
            };

            await this.movieCommentsRepository.AddAsync(reply);
            await this.movieCommentsRepository.SaveChangesAsync();
        }
    }
}
