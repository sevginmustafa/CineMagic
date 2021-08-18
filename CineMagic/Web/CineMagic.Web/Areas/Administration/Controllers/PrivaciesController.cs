namespace CineMagic.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using CineMagic.Web.ViewModels.Privacies;
    using Microsoft.AspNetCore.Mvc;

    public class PrivaciesController : AdministrationController
    {
        private readonly IPrivaciesService privaciesService;

        public PrivaciesController(IPrivaciesService privaciesService)
        {
            this.privaciesService = privaciesService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PrivacyCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.privaciesService.CreateAsync(inputModel);

            return this.RedirectToAction("Index", "Privacies");
        }

        public async Task<IActionResult> Edit()
        {
            var viewModel = await this.privaciesService
                .GetViewModelAsync<PrivacyEditViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PrivacyEditViewModel privacyEditViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(privacyEditViewModel);
            }

            await this.privaciesService.EditAsync(privacyEditViewModel);

            return this.RedirectToAction("Index", "Privacies");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.privaciesService.DeleteAsync(id);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
