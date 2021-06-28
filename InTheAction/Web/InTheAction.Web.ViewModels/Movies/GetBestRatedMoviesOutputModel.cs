namespace InTheAction.Web.ViewModels.Movies
{
    public class GetBestRatedMoviesOutputModel
    {
        public string Title { get; set; }

        public string CoverImageUrl { get; set; }

        public short ReleaseYear { get; set; }

        public string Runtime { get; set; }

        public string Description { get; set; }

        public double Rating { get; set; }

        public int NumberOfVotes { get; set; }

        public string Language { get; set; }

        public double Budget { get; set; }
    }
}
