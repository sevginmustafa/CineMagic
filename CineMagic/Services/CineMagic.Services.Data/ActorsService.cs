namespace CineMagic.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Common;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Services.Mapping;
    using CineMagic.Web.ViewModels.Actors;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.EntityFrameworkCore;

    public class ActorsService : IActorsService
    {
        private const string AllPaginationFilter = "All";
        private const string DigitPaginationFilter = "0 - 9";

        private readonly IDeletableEntityRepository<Actor> actorsRepository;

        public ActorsService(IDeletableEntityRepository<Actor> actorsRepository)
        {
            this.actorsRepository = actorsRepository;
        }

        public async Task<T> GetActorByIdAsync<T>(int id)
        {
            var actor = await this.actorsRepository
            .AllAsNoTracking()
            .Where(x => x.Id == id)
            .To<T>()
            .FirstOrDefaultAsync();

            if (actor == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.ActorNotFound, id));
            }

            return actor;
        }

        public IQueryable<T> GetActorsByLetterAsQueryable<T>(string letter)
        {
            var actors = Enumerable.Empty<T>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(letter) && letter != AllPaginationFilter && letter != DigitPaginationFilter)
            {
                actors = this.actorsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Name.ToLower().StartsWith(letter.ToLower()))
                    .OrderBy(x => x.Name)
                    .To<T>();
            }
            else if (letter == DigitPaginationFilter)
            {
                var digits = Enumerable.Range(0, 10).Select(x => x.ToString()).ToList();

                actors = this.actorsRepository
                    .AllAsNoTracking()
                    .Where(x => digits.Contains(x.Name.Substring(0, 1)))
                    .OrderBy(x => x.Name)
                    .To<T>();
            }
            else
            {
                actors = this.GetAllActorsAsQueryableOrderedByName<T>();
            }

            return actors;
        }

        public IQueryable<T> SearchActorsByTitleAsQueryable<T>(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return this.actorsRepository
                     .AllAsNoTracking()
                     .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                     .OrderBy(x => x.Name)
                     .To<T>();
            }

            return this.GetAllActorsAsQueryableOrderedByName<T>();
        }

        public IQueryable<T> GetActorsBornTodayAsQueryable<T>(int gender)
        {
            var actors = Enumerable.Empty<T>().AsQueryable();

            if (gender == 0)
            {
                actors = this.actorsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Birthday.Value.DayOfYear == DateTime.UtcNow.DayOfYear)
                    .OrderBy(x => x.Name)
                    .To<T>();
            }
            else
            {
                actors = this.actorsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Birthday.Value.DayOfYear == DateTime.UtcNow.DayOfYear && (int)x.Gender == gender)
                    .OrderBy(x => x.Name)
                    .To<T>();
            }

            return actors;
        }

        public IQueryable<T> GetMostPopularActorsAsQueryable<T>(int gender, int count)
        {
            var actors = Enumerable.Empty<T>().AsQueryable();

            if (gender == 0)
            {
                actors = this.actorsRepository
                    .AllAsNoTracking()
                    .OrderByDescending(x => x.Popularity)
                    .Take(count)
                    .To<T>();
            }
            else
            {
                actors = this.actorsRepository
                    .AllAsNoTracking()
                    .Where(x => (int)x.Gender == gender)
                    .OrderByDescending(x => x.Popularity)
                    .Take(count)
                    .To<T>();
            }

            return actors;
        }

        public async Task CreateAsync(ActorCreateInputModel inputModel)
        {
            bool findActor = await this.actorsRepository
                  .AllAsNoTracking()
                  .AnyAsync(x => x.Name == inputModel.Name);

            if (findActor)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.ActorAlreadyExists, inputModel.Name));
            }

            var actor = new Actor
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

            await this.actorsRepository.AddAsync(actor);
            await this.actorsRepository.SaveChangesAsync();
        }

        public IQueryable<T> GetAllActorsAsQueryableOrderedByName<T>()
        => this.actorsRepository
            .AllAsNoTracking()
            .OrderBy(x => x.Name)
            .To<T>();

        public IQueryable<T> GetAllActorsAsQueryableOrderedByCreatedOn<T>()
         => this.actorsRepository
             .AllAsNoTracking()
             .OrderByDescending(x => x.CreatedOn)
             .To<T>();

        public async Task DeleteAsync(int id)
        {
            var actor = await this.actorsRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (actor == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.ActorNotFound, id));
            }

            actor.IsDeleted = true;
            actor.DeletedOn = DateTime.UtcNow;

            this.actorsRepository.Update(actor);
            await this.actorsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(ActorEditViewModel actorEditViewModel)
        {
            var actor = await this.actorsRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == actorEditViewModel.Id);

            if (actor == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.ActorNotFound, actorEditViewModel.Id));
            }

            actor.Name = actorEditViewModel.Name;
            actor.ProfilePicPath = actorEditViewModel.ProfilePicPath;
            actor.Biography = actorEditViewModel.Biography;
            actor.Gender = actorEditViewModel.Gender;
            actor.Birthday = actorEditViewModel.Birthday;
            actor.Deathday = actorEditViewModel.Deathday;
            actor.Birthplace = actorEditViewModel.Birthplace;
            actor.Popularity = actorEditViewModel.Popularity;

            this.actorsRepository.Update(actor);
            await this.actorsRepository.SaveChangesAsync();
        }

        public async Task<T> GetViewModelByIdAsync<T>(int id)
        {
            var actor = await this.actorsRepository
                 .AllAsNoTracking()
                 .Where(m => m.Id == id)
                 .To<T>()
                 .FirstOrDefaultAsync();

            if (actor == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.ActorNotFound, id));
            }

            return actor;
        }
    }
}
