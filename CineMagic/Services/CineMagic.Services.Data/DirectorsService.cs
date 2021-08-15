﻿namespace CineMagic.Services.Data
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
    using CineMagic.Web.ViewModels.Directors;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.EntityFrameworkCore;

    public class DirectorsService : IDirectorsService
    {
        private readonly IDeletableEntityRepository<Director> directorsRepository;

        public DirectorsService(IDeletableEntityRepository<Director> directorsRepository)
        {
            this.directorsRepository = directorsRepository;
        }

        public async Task<T> GetDirectorByIdAsync<T>(int id)
        {
            var director = await this.directorsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            if (director == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.DirectorNotFound, id));
            }

            return director;
        }

        public IQueryable<T> GetDirectorsBornTodayAsQueryable<T>(int gender)
        {
            var directors = Enumerable.Empty<T>().AsQueryable();

            if (gender == 0)
            {
                directors = this.directorsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Birthday.Value.DayOfYear == DateTime.Now.DayOfYear)
                    .OrderBy(x => x.Name)
                    .To<T>();
            }
            else
            {
                directors = this.directorsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Birthday.Value.DayOfYear == DateTime.Now.DayOfYear && (int)x.Gender == gender)
                    .OrderBy(x => x.Name)
                    .To<T>();
            }

            return directors;
        }

        public IQueryable<T> GetMostPopularDirectorsAsQueryable<T>(int gender, int count)
        {
            var directors = Enumerable.Empty<T>().AsQueryable();

            if (gender == 0)
            {
                directors = this.directorsRepository
                    .AllAsNoTracking()
                    .OrderByDescending(x => x.Popularity)
                    .Take(count)
                    .To<T>();
            }
            else
            {
                directors = this.directorsRepository
                    .AllAsNoTracking()
                    .Where(x => (int)x.Gender == gender)
                    .OrderByDescending(x => x.Popularity)
                    .Take(count)
                    .To<T>();
            }

            return directors;
        }

        public async Task CreateAsync(DirectorCreateInpuModel inputModel)
        {
            bool findDirector = await this.directorsRepository
                   .AllAsNoTracking()
                   .AnyAsync(x => x.Name == inputModel.Name);

            if (findDirector)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.DirectorAlreadyExists, inputModel.Name));
            }

            var director = new Director
            {
                Name = inputModel.Name,
                ProfilePicPath = inputModel.ProfilePicPath,
                Biography = inputModel.Biography,
                Gender = inputModel.Gender,
                Birthday = inputModel.Birthday,
                Deathday = inputModel.Deathday,
                Birthplace = inputModel.Birthplace,
                Popularity = inputModel.Popularity,
            };

            await this.directorsRepository.AddAsync(director);
            await this.directorsRepository.SaveChangesAsync();
        }

        public IQueryable<T> GetAllDirectorsAsQueryable<T>()
        => this.directorsRepository
            .AllAsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .To<T>();

        public async Task DeleteAsync(int id)
        {
            var director = await this.directorsRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (director == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.DirectorNotFound, id));
            }

            director.IsDeleted = true;
            director.DeletedOn = DateTime.UtcNow;

            this.directorsRepository.Update(director);
            await this.directorsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(DirectorEditViewModel directorEditViewModel)
        {
            var director = await this.directorsRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == directorEditViewModel.Id);

            if (director == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.DirectorNotFound, directorEditViewModel.Id));
            }

            director.Name = directorEditViewModel.Name;
            director.ProfilePicPath = directorEditViewModel.ProfilePicPath;
            director.Biography = directorEditViewModel.Biography;
            director.Gender = directorEditViewModel.Gender;
            director.Birthday = directorEditViewModel.Birthday;
            director.Deathday = directorEditViewModel.Deathday;
            director.Birthplace = directorEditViewModel.Birthplace;
            director.Popularity = directorEditViewModel.Popularity;

            this.directorsRepository.Update(director);
            await this.directorsRepository.SaveChangesAsync();
        }

        public async Task<T> GetViewModelByIdAsync<T>(int id)
        {
            var director = await this.directorsRepository
                 .AllAsNoTracking()
                 .Where(m => m.Id == id)
                 .To<T>()
                 .FirstOrDefaultAsync();

            if (director == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.DirectorNotFound, id));
            }

            return director;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
            => await this.directorsRepository
            .AllAsNoTracking()
            .OrderBy(x => x.Name)
            .To<T>()
            .ToListAsync();
    }
}
