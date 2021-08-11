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
    }
}
