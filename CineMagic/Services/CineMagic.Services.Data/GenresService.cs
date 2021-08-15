namespace CineMagic.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Common;
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

            if (findGenre)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.GenreAlreadyExists, inputModel.Name));
            }

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
            var genre = await this.genresRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (genre == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.GenreNotFound, id));
            }

            genre.IsDeleted = true;
            genre.DeletedOn = DateTime.UtcNow;

            this.genresRepository.Update(genre);
            await this.genresRepository.SaveChangesAsync();
        }

        public async Task EditAsync(GenreEditViewModel genreEditViewModel)
        {
            var genre = await this.genresRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == genreEditViewModel.Id);

            if (genre == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.GenreNotFound, genreEditViewModel.Id));
            }

            genre.Name = genreEditViewModel.Name;

            this.genresRepository.Update(genre);
            await this.genresRepository.SaveChangesAsync();
        }

        public async Task<T> GetViewModelByIdAsync<T>(int id)
        {
            var genre = await this.genresRepository
                 .AllAsNoTracking()
                 .Where(m => m.Id == id)
                 .To<T>()
                 .FirstOrDefaultAsync();

            if (genre == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.GenreNotFound, id));
            }

            return genre;
        }
    }
}
