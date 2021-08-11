namespace CineMagic.Web.ViewModels.Privacies
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;
    using Ganss.XSS;

    public class PrivacyContentViewModel : IMapFrom<Privacy>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);
    }
}
