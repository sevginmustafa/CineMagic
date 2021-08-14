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
    using CineMagic.Services.Mapping;
    using CineMagic.Web.ViewModels.Genres;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.EntityFrameworkCore;

    public class GenresService : IGenresService
    {
        private readonly IDeletableEntityRepository<Genre> genresRepository;

        public GenresService(IDeletableEntityRepository<Genre> genresRepository)
        {
            this.genresRepository = genresRepository;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
             => await this.genresRepository
             .AllAsNoTracking()
             .OrderBy(x => x.Name)
             .To<T>()
             .ToListAsync();

        public async Task<IEnumerable<T>> GetPopularGenresAsync<T>()
        {
            var popularGenres = await this.genresRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Movies.Count)
                .To<T>()
                .ToListAsync();

            return popularGenres;
        }

        public async Task CreateAsync(GenreCreateInputModel inputModel)
        {
            bool findGenre = await this.genresRepository
                   .AllAsNoTracking()
                   .AnyAsync(x => x.Name == inputModel.Name);

            //if (findDirector)
            //{
            //    throw new ArgumentException(
            //        string.Format(ExceptionMessages.DirectorAlreadyExists, actor.FirstName, actor.LastName));
            //}

            var genre = new Genre
            {
                Name = inputModel.Name,
            };

            await this.genresRepository.AddAsync(genre);
            await this.genresRepository.SaveChangesAsync();
        }

        public IQueryable<T> GetAllGenresAsQueryable<T>()
        => this.genresRepository
            .AllAsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .To<T>();

        public async Task DeleteAsync(int id)
        {
            var findGenre = await this.genresRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            // if (findDirector == null)
            // {
            //     throw new ArgumentException(
            //string.Format(ExceptionMessages.DirectorAlreadyExists, actor.FirstName, actor.LastName));
            // }

            findGenre.IsDeleted = true;
            findGenre.DeletedOn = DateTime.UtcNow;

            this.genresRepository.Update(findGenre);
            await this.genresRepository.SaveChangesAsync();
        }

        public async Task EditAsync(GenreEditViewModel genreEditViewModel)
        {
            var findGenre = await this.genresRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == genreEditViewModel.Id);

            // if (findActor == null)
            // {
            //     throw new ArgumentException(
            //string.Format(ExceptionMessages.DirectorAlreadyExists, actor.FirstName, actor.LastName));
            // }

            findGenre.Name = genreEditViewModel.Name;

            this.genresRepository.Update(findGenre);
            await this.genresRepository.SaveChangesAsync();
        }

        public async Task<T> GetViewModelByIdAsync<T>(int id)
        {
            var genre = await this.genresRepository
                 .AllAsNoTracking()
                 .Where(m => m.Id == id)
                 .To<T>()
                 .FirstOrDefaultAsync();

            //if (director == null)
            //{
            //    throw new NullReferenceException(string.Format(ExceptionMessages.MovieNotFound, id));
            //}

            return genre;
        }
    }
}
