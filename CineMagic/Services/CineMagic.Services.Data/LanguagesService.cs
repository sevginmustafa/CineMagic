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
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using CineMagic.Web.ViewModels.Languages;
    using Microsoft.EntityFrameworkCore;

    public class LanguagesService : ILanguagesService
    {
        private readonly IDeletableEntityRepository<Language> languagesRepository;

        public LanguagesService(IDeletableEntityRepository<Language> languagesRepository)
        {
            this.languagesRepository = languagesRepository;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
            => await this.languagesRepository
            .AllAsNoTracking()
            .OrderBy(x => x.Name)
            .To<T>()
            .ToListAsync();

        public async Task CreateAsync(LanguageCreateInputModel inputModel)
        {
            bool findLanguage = await this.languagesRepository
                   .AllAsNoTracking()
                   .AnyAsync(x => x.Name == inputModel.Name);

            if (findLanguage)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.LanguageAlreadyExists, inputModel.Name));
            }

            var language = new Language
            {
                Name = inputModel.Name,
            };

            await this.languagesRepository.AddAsync(language);
            await this.languagesRepository.SaveChangesAsync();
        }

        public IQueryable<T> GetAllLanguagesAsQueryable<T>()
        => this.languagesRepository
            .AllAsNoTracking()
            .OrderByDescending(x => x.CreatedOn)
            .To<T>();

        public async Task DeleteAsync(int id)
        {
            var language = await this.languagesRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (language == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.LanguageNotFound, id));
            }

            language.IsDeleted = true;
            language.DeletedOn = DateTime.UtcNow;

            this.languagesRepository.Update(language);
            await this.languagesRepository.SaveChangesAsync();
        }

        public async Task EditAsync(LanguageEditViewModel languageEditViewModel)
        {
            var language = await this.languagesRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == languageEditViewModel.Id);

            if (language == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.LanguageNotFound, languageEditViewModel.Id));
            }

            language.Name = languageEditViewModel.Name;

            this.languagesRepository.Update(language);
            await this.languagesRepository.SaveChangesAsync();
        }

        public async Task<T> GetViewModelByIdAsync<T>(int id)
        {
            var language = await this.languagesRepository
                 .AllAsNoTracking()
                 .Where(m => m.Id == id)
                 .To<T>()
                 .FirstOrDefaultAsync();

            if (language == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.LanguageNotFound, id));
            }

            return language;
        }
    }
}
