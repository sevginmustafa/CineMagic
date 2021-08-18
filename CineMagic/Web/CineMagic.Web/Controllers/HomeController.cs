namespace CineMagic.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.Infrastructure;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Movies;
    using CineMagic.Web.ViewModels.Privacies;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class HomeController : Controller
    {
        private const int HomePageLeadMovieCount = 1;
        private const int HomePageTabFeaturedMoviesCount = 13;
        private const int HomePageLatestMoviesCount = 10;
        private const int HomePageWatchlistMoviesCount = 10;

        private readonly IMoviesService moviesService;
        private readonly IPrivaciesService privaciesService;
        private readonly IMemoryCache cache;

        public HomeController(
            IMoviesService moviesService,
            IPrivaciesService privaciesService,
            IMemoryCache cache)
        {
            this.moviesService = moviesService;
            this.privaciesService = privaciesService;
            this.cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            const string bannerSectionMovieMovieCacheKey = "BannerSectionMovieMovieCacheKey";
            const string mostRecentMovieCacheKey = "MostRecentMovieCacheKey";
            const string recentMoviesCacheKey = "RecentMoviesCacheKey";
            const string mostPopularMovieCacheKey = "MostPopularMovieCacheKey";
            const string popularMoviesCacheKey = "PopularMoviesCacheKey";
            const string bestRatedMovieCacheKey = "BestRatedMovieCacheKey";
            const string topRatedMoviesCacheKey = "TopRatedMoviesCacheKey";
            const string latestMoviesCacheKey = "LatestMoviesCacheKey";

            var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

            var bannerSectionMovie = this.cache.Get<MovieHomePageBannerViewModel>(bannerSectionMovieMovieCacheKey);

            if (bannerSectionMovie == null)
            {
                bannerSectionMovie = await this.moviesService
                    .GetBannerSectionMovieAsync<MovieHomePageBannerViewModel>();

                this.cache.Set(bannerSectionMovieMovieCacheKey, bannerSectionMovie, cacheOptions);
            }

            var mostRecentMovie = this.cache.Get<IEnumerable<MovieRespTabsViewModel>>(mostRecentMovieCacheKey);

            if (mostRecentMovie == null)
            {
                mostRecentMovie = await this.moviesService
                    .GetRecentMoviesAsync<MovieRespTabsViewModel>(HomePageLeadMovieCount);

                this.cache.Set(mostRecentMovieCacheKey, mostRecentMovie, cacheOptions);
            }

            var recentMovies = this.cache.Get<IEnumerable<MovieStandartViewModel>>(recentMoviesCacheKey);

            if (recentMovies == null)
            {
                recentMovies = await this.moviesService
                    .GetRecentMoviesAsync<MovieStandartViewModel>(HomePageTabFeaturedMoviesCount);

                this.cache.Set(recentMoviesCacheKey, recentMovies, cacheOptions);
            }

            var mostPopularMovie = this.cache.Get<IEnumerable<MovieRespTabsViewModel>>(mostPopularMovieCacheKey);

            if (mostPopularMovie == null)
            {
                mostPopularMovie = await this.moviesService
                    .GetPopularMoviesAsync<MovieRespTabsViewModel>(HomePageLeadMovieCount);

                this.cache.Set(mostPopularMovieCacheKey, mostPopularMovie, cacheOptions);
            }

            var popularMovies = this.cache.Get<IEnumerable<MovieStandartViewModel>>(popularMoviesCacheKey);

            if (popularMovies == null)
            {
                popularMovies = await this.moviesService
                    .GetPopularMoviesAsync<MovieStandartViewModel>(HomePageTabFeaturedMoviesCount);

                this.cache.Set(popularMoviesCacheKey, popularMovies, cacheOptions);
            }

            var bestRatedMovie = this.cache.Get<IEnumerable<MovieRespTabsViewModel>>(bestRatedMovieCacheKey);

            if (bestRatedMovie == null)
            {
                bestRatedMovie = await this.moviesService
                    .GetTopRatedMoviesAsync<MovieRespTabsViewModel>(HomePageLeadMovieCount);

                this.cache.Set(bestRatedMovieCacheKey, bestRatedMovie, cacheOptions);
            }

            var topRatedMovies = this.cache.Get<IEnumerable<MovieStandartViewModel>>(topRatedMoviesCacheKey);

            if (topRatedMovies == null)
            {
                topRatedMovies = await this.moviesService
                    .GetTopRatedMoviesAsync<MovieStandartViewModel>(HomePageTabFeaturedMoviesCount);

                this.cache.Set(topRatedMoviesCacheKey, topRatedMovies, cacheOptions);
            }

            var latestMovies = this.cache.Get<IEnumerable<MovieStandartViewModel>>(latestMoviesCacheKey);

            if (latestMovies == null)
            {
                latestMovies = await this.moviesService
                    .GetLatestMoviesAsync<MovieStandartViewModel>(HomePageLatestMoviesCount);

                this.cache.Set(latestMoviesCacheKey, latestMovies, cacheOptions);
            }

            var userId = this.User.GetId();

            var watchlistMovies = await this.moviesService.GetWatchlistMoviesAsync<MovieWatchlistViewModel>(userId, HomePageWatchlistMoviesCount);

            var viewModels = new HomePageViewModelsList
            {
                BannerSectionMovie = bannerSectionMovie,
                MostRecentMovie = mostRecentMovie,
                RecentMovies = recentMovies.Skip(1),
                MostPopularMovie = mostPopularMovie,
                PopularMovies = popularMovies.Skip(1),
                BestRatedMovie = bestRatedMovie,
                TopRatedMovies = topRatedMovies.Skip(1),
                LatestMovies = latestMovies,
                Watchlist = watchlistMovies,
            };

            return this.View(viewModels);
        }

        public async Task<IActionResult> Privacy()
        {
            var privacy = await this.privaciesService.GetPrivacyContentAsync<PrivacyContentViewModel>();

            return this.View(privacy);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        [Route("/Home/HandleError/{code:int}")]
        public IActionResult HandleError(int code)
        {
            return this.View("~/Views/Shared/404.cshtml");
        }
    }
}
