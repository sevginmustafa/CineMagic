﻿namespace CineMagic.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICountriesService
    {
        Task<IEnumerable<T>> GetPopularCountriesAsync<T>();

        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}
