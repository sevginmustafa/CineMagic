namespace CineMagic.Services.Data
{
    using System.Linq;

    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Services.Mapping;

    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;
        private readonly IDeletableEntityRepository<Actor> actorsRrepository;
        private readonly IDeletableEntityRepository<Director> directorsRepository;

        public SearchService(
            IDeletableEntityRepository<Movie> moviesRepository,
            IDeletableEntityRepository<Actor> actorsRrepository,
            IDeletableEntityRepository<Director> directorsRepository)
        {
            this.moviesRepository = moviesRepository;
            this.actorsRrepository = actorsRrepository;
            this.directorsRepository = directorsRepository;
        }

        public IQueryable<T> SearchMoviesAsQueryable<T>(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                return this.moviesRepository
                    .AllAsNoTracking()
                    .Where(x => x.Title.ToLower().Contains(title.ToLower()))
                    .To<T>();
            }

            return this.moviesRepository
                .AllAsNoTracking()
                .To<T>();
        }

        public IQueryable<T> SearchActorsAsQueryable<T>(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return this.actorsRrepository
                    .AllAsNoTracking()
                    .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                    .To<T>();
            }

            return this.actorsRrepository
                .AllAsNoTracking()
                .To<T>();
        }

        public IQueryable<T> SearchDirectorsAsQueryable<T>(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return this.directorsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                    .To<T>();
            }

            return this.directorsRepository
                .AllAsNoTracking()
                .To<T>();
        }
    }
}
