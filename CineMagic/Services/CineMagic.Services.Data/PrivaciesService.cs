namespace CineMagic.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Common;
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
            bool findPrivacy = await this.privaciesRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Content == inputModel.Content);

            if (findPrivacy)
            {
                throw new ArgumentException(ExceptionMessages.PrivacyAlreadyExists);
            }

            var privacy = new Privacy
            {
                Content = inputModel.Content,
            };

            await this.privaciesRepository.AddAsync(privacy);
            await this.privaciesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var privacy = await this.privaciesRepository
                           .AllAsNoTracking()
                           .FirstOrDefaultAsync(x => x.Id == id);

            if (privacy == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PrivacyNotFound, id));
            }

            privacy.IsDeleted = true;
            privacy.DeletedOn = DateTime.UtcNow;

            this.privaciesRepository.Update(privacy);
            await this.privaciesRepository.SaveChangesAsync();
        }

        public async Task EditAsync(PrivacyEditViewModel privacyEditViewModel)
        {
            var privacy = await this.privaciesRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == privacyEditViewModel.Id);

            if (privacy == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.PrivacyNotFound, privacyEditViewModel.Id));
            }

            privacy.Content = privacyEditViewModel.Content;

            this.privaciesRepository.Update(privacy);
            await this.privaciesRepository.SaveChangesAsync();
        }

        public async Task<T> GetViewModelAsync<T>()
        {
            var privacy = await this.privaciesRepository
                .AllAsNoTracking()
                .To<T>()
                .FirstOrDefaultAsync();

            if (privacy == null)
            {
                throw new NullReferenceException(ExceptionMessages.PrivacyViewModelNotFound);
            }

            return privacy;
        }
    }
}
