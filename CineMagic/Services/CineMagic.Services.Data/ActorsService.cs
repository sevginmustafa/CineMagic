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
    using CineMagic.Web.ViewModels.Actors;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.EntityFrameworkCore;

    public class ActorsService : IActorsService
    {
        private readonly IDeletableEntityRepository<Actor> actorsRepository;

        public ActorsService(IDeletableEntityRepository<Actor> actorsRepository)
        {
            this.actorsRepository = actorsRepository;
        }

        public async Task<T> GetActorByIdAsync<T>(int id)
         => await this.actorsRepository
            .AllAsNoTracking()
            .Where(x => x.Id == id)
            .To<T>()
            .FirstOrDefaultAsync();

        public IQueryable<T> GetActorsBornTodayAsQueryable<T>(int gender)
        {
            var actors = Enumerable.Empty<T>().AsQueryable();

            if (gender == 0)
            {
                actors = this.actorsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Birthday.Value.DayOfYear == DateTime.Now.DayOfYear)
                    .OrderBy(x => x.Name)
                    .To<T>();
            }
            else
            {
                actors = this.actorsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Birthday.Value.DayOfYear == DateTime.Now.DayOfYear && (int)x.Gender == gender)
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

            //if (findActor)
            //{
            //    throw new ArgumentException(
            //        string.Format(ExceptionMessages.DirectorAlreadyExists, actor.FirstName, actor.LastName));
            //}

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

        public IQueryable<T> GetAllActorsAsQueryable<T>()
        => this.actorsRepository
            .AllAsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .To<T>();

        public async Task DeleteAsync(int id)
        {
            var findActor = await this.actorsRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            // if (findActor == null)
            // {
            //     throw new ArgumentException(
            //string.Format(ExceptionMessages.DirectorAlreadyExists, actor.FirstName, actor.LastName));
            // }

            findActor.IsDeleted = true;
            findActor.DeletedOn = DateTime.UtcNow;

            this.actorsRepository.Update(findActor);
            await this.actorsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(ActorEditViewModel actorEditViewModel)
        {
            var findActor = await this.actorsRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == actorEditViewModel.Id);

            // if (findActor == null)
            // {
            //     throw new ArgumentException(
            //string.Format(ExceptionMessages.DirectorAlreadyExists, actor.FirstName, actor.LastName));
            // }

            findActor.Name = actorEditViewModel.Name;
            findActor.ProfilePicPath = actorEditViewModel.ProfilePicPath;
            findActor.Biography = actorEditViewModel.Biography;
            findActor.Gender = actorEditViewModel.Gender;
            findActor.Birthday = actorEditViewModel.Birthday;
            findActor.Deathday = actorEditViewModel.Deathday;
            findActor.Birthplace = actorEditViewModel.Birthplace;
            findActor.Popularity = actorEditViewModel.Popularity;

            this.actorsRepository.Update(findActor);
            await this.actorsRepository.SaveChangesAsync();
        }

        public async Task<T> GetViewModelByIdAsync<T>(int id)
        {
            var actor = await this.actorsRepository
                 .AllAsNoTracking()
                 .Where(m => m.Id == id)
                 .To<T>()
                 .FirstOrDefaultAsync();

            //if (actor == null)
            //{
            //    throw new NullReferenceException(string.Format(ExceptionMessages.MovieNotFound, id));
            //}

            return actor;
        }
    }
}
