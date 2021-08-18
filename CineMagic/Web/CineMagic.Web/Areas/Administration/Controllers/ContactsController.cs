namespace CineMagic.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CineMagic.Common;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Contacts;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : AdministrationController
    {
        private readonly IContactsService contactsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ContactsController(
            IContactsService contactsService,
            UserManager<ApplicationUser> userManager)
        {
            this.contactsService = contactsService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult SuccessMessage()
        {
            return this.View();
        }

        public async Task<IActionResult> Answer(int id)
        {
            var enquiry = this.contactsService.GetEnquiryById(id);

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new AdminContactFormInputModel
            {
                Name = user.UserName,
                Email = user.Email,
                To = enquiry.Email,
                Subject = "RE: " + enquiry.Subject,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Answer(AdminContactFormInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.contactsService.SendAnswerToUserAsync(inputModel);

            return this.RedirectToAction(nameof(this.SuccessMessage), new { area = "Administration" });
        }

        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new AdminContactFormInputModel
            {
                Name = user.UserName,
                Email = user.Email,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminContactFormInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.contactsService.SendAnswerToUserAsync(inputModel);

            return this.RedirectToAction(nameof(this.SuccessMessage), new { area = "Administration" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.contactsService.DeleteAsync(id);

            return this.RedirectToAction("GetAll", "Contacts", new { area = "Administration" });
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.contactsService
                .GetViewModelByIdAsync<ContactDetailedViewModel>(id);

            return this.View(viewModel);
        }

        public async Task<IActionResult> GetAll(int page = 1)
        {
            var enquiries = this.contactsService
                .GetAllEnquiriesFromUsersAsQueryable<ContactsAdministrationViewModel>();

            var paginatedList = await PaginatedList<ContactsAdministrationViewModel>
                .CreateAsync(enquiries, page, GlobalConstants.AdministrationItemsPerPage);

            return this.View(paginatedList);
        }
    }
}
