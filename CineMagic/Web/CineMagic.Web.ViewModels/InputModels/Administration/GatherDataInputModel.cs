namespace CineMagic.Web.ViewModels.InputModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    using static CineMagic.Common.ModelValidation.GatherMovies;

    public class GatherDataInputModel
    {
        [Display(Name = StartIndexDisplayName)]
        public int StartIndex { get; set; }

        [Display(Name = EndIndexDisplayName)]
        public int EndIndex { get; set; }
    }
}
