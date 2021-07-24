namespace CineMagic.Web.ViewModels.Directors
{
    using CineMagic.Data.Models.Enums;

    public class DirectorsGenderFilterPagingViewModel
    {
        public PaginatedList<DirectorStandartViewModel> Directors { get; set; }

        public Gender Gender { get; set; }
    }
}
