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
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using CineMagic.Web.ViewModels.Privacies;
    using Microsoft.EntityFrameworkCore;

    public class PrivaciesService : IPrivaciesService
    {
        private readonly IDeletableEntityRepository<Privacy> privaciesRepository;

        public PrivaciesService(IDeletableEntityRepository<Privacy> privaciesRepository)
        {
            this.privaciesRepository = privaciesRepository;
        }

        public async Task<T> GetPrivacyContentAsync<T>()
            => await this.privaciesRepository
            .AllAsNoTracking()
            .To<T>()
            .FirstOrDefaultAsync();

        public async Task CreateAsync(PrivacyCreateInputModel inputModel)
        {
            if (this.privaciesRepository.AllAsNoTracking().Any())
            {
                // TODO
            }

            //if (findDirector)
            //{
            //    throw new ArgumentException(
            //        string.Format(ExceptionMessages.DirectorAlreadyExists, actor.FirstName, actor.LastName));
            //}

            var privacy = new Privacy
            {
                Content = inputModel.Content,
            };

            await this.privaciesRepository.AddAsync(privacy);
            await this.privaciesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var findPrivacy = await this.privaciesRepository
                           .AllAsNoTracking()
                           .FirstOrDefaultAsync(x => x.Id == id);

            // if (findDirector == null)
            // {
            //     throw new ArgumentException(
            //string.Format(ExceptionMessages.DirectorAlreadyExists, actor.FirstName, actor.LastName));
            // }

            findPrivacy.IsDeleted = true;
            findPrivacy.DeletedOn = DateTime.UtcNow;

            this.privaciesRepository.Update(findPrivacy);
            await this.privaciesRepository.SaveChangesAsync();
        }

        public async Task EditAsync(PrivacyEditViewModel privacyEditViewModel)
        {
            var findPrivacy = await this.privaciesRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == privacyEditViewModel.Id);

            // if (findActor == null)
            // {
            //     throw new ArgumentException(
            //string.Format(ExceptionMessages.DirectorAlreadyExists, actor.FirstName, actor.LastName));
            // }

            findPrivacy.Content = privacyEditViewModel.Content;

            this.privaciesRepository.Update(findPrivacy);
            await this.privaciesRepository.SaveChangesAsync();
        }

        public async Task<T> GetViewModelAsync<T>()
        {
            var privacy = await this.privaciesRepository
                .AllAsNoTracking()
                .To<T>()
                .FirstOrDefaultAsync();

            //if (director == null)
            //{
            //    throw new NullReferenceException(string.Format(ExceptionMessages.MovieNotFound, id));
            //}

            return privacy;
        }
    }
}
