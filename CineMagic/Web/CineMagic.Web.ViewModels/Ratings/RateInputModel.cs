namespace CineMagic.Web.ViewModels.Ratings
{
    using System.ComponentModel.DataAnnotations;

    public class RateInputModel
    {
        [Range(1, 5)]
        public int Rate { get; set; }

        public int MovieId { get; set; }
    }
}
