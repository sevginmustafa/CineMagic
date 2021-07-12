namespace InTheAction.Services.GetDataFromTMDB
{
    using InTheAction.Services.GetDataFromTMDB.DTOs;

    public interface IGetDataFromTMDBService
    {
        MovieDTO GetMovieDataAsJSON(int movieId);

        string GetMovieTrailerPathDataAsJSON(int movieId);

        CastAndCrewDTO GetMovieCastAndCrewDataAsJSON(int movieId);

        PersonDTO GetMovieActorDataAsJSON(int actorId);

        PersonDTO GetMovieDirectorDataAsJSON(CastAndCrewDTO castAndCrew);
    }
}
