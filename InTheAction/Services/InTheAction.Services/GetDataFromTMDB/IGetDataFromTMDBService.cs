namespace InTheAction.Services.GetDataFromTMDB
{
    using System.Collections.Generic;

    using InTheAction.Services.GetDataFromTMDB.DTOs;

    public interface IGetDataFromTMDBService
    {
        IEnumerable<MovieDTO> GetMovieDataAsJSON(int startIndex, int endIndex);

        string GetMovieTrailerPathDataAsJSON(int movieId);

        CastAndCrewDTO GetMovieCastAndCrewDataAsJSON(int movieId);

        PersonDTO GetMovieActorDataAsJSON(int actorId);

        PersonDTO GetMovieDirectorDataAsJSON(CastAndCrewDTO castAndCrew);
    }
}
