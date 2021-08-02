namespace CineMagic.Web.ViewModels.Ratings
{
    using System.ComponentModel.DataAnnotations;

    public class RateInputModel
    {
        [Range(0, 5)]
        public double Rate { get; set; }

        public int MovieId { get; set; }
    }
}
