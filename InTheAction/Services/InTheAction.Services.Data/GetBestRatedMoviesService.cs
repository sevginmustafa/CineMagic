namespace InTheAction.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using InTheAction.Data.Common.Repositories;
    using InTheAction.Data.Models;
    using InTheAction.Services.Data.DTOs;

    public class GetBestRatedMoviesService : IGetBestRatedMoviesService
    {
        private readonly IDeletableEntityRepository<Movie> moviesRepository;

        public GetBestRatedMoviesService(IDeletableEntityRepository<Movie> moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        public IEnumerable<BestRatedMoviesDTO> Get()
        {
            return this.moviesRepository.All()
                .OrderByDescending(x => x.Rating)
                .Select(x => new BestRatedMoviesDTO
                {
                    Title = x.Title,
                    CoverImageUrl = x.CoverImageUrl,
                    ReleaseYear = x.ReleaseYear,
                    Runtime = x.Runtime,
                    Description = x.Description,
                    Rating = x.Rating,
                    NumberOfVotes = x.NumberOfVotes,
                    Language = x.Language,
                    Budget = x.Budget,
                })
                .ToList();
        }
    }
}
