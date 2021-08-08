namespace CineMagic.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels.InputModels.Contacts;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : Controller
    {
        private readonly IContactsService contactsService;

        public ContactsController(IContactsService contactsService)
        {
            this.contactsService = contactsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.contactsService.GetEnquiryFromUser(inputModel);

            return this.Redirect("/");
        }
    }
}
