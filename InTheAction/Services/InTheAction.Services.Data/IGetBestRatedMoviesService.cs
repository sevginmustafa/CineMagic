namespace InTheAction.Services.Data
{
    using System.Collections.Generic;

    using InTheAction.Services.Data.DTOs;

    public interface IGetBestRatedMoviesService
    {
        IEnumerable<BestRatedMoviesDTO> Get();
    }
}
