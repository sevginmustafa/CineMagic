namespace CineMagic.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class MovieHomePageBannerViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Backdrops { get; set; }

        public string TrailerPath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieHomePageBannerViewModel>()
                .ForMember(m => m.Backdrops, opt => opt.MapFrom(x => string.Join(", ", x.Backdrops.Select(x => "\"" + x.Path + "\""))));
                
        }
    }
}
