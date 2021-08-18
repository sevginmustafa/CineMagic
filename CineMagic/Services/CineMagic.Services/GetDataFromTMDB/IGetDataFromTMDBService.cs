namespace CineMagic.Services.GetDataFromTMDB
{
    using CineMagic.Services.GetDataFromTMDB.DTOs;

    public interface IGetDataFromTMDBService
    {
        MovieDTO GetMovieDataAsJSON(int movieId);

        public BackdropsDTO GetMovieBackdropsDataAsJSON(int movieId);

        string GetMovieTrailerPathDataAsJSON(int movieId);

        CastAndCrewDTO GetMovieCastAndCrewDataAsJSON(int movieId);

        PersonDTO GetMovieActorDataAsJSON(int actorId);

        PersonDTO GetMovieDirectorDataAsJSON(CastAndCrewDTO castAndCrew);
    }
}
