namespace CineMagic.Web.ViewModels.Contacts
{
    using CineMagic.Data.Models;
    using CineMagic.Services.Mapping;

    public class ContactDetailedViewModel : IMapFrom<ContactFormEntry>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
