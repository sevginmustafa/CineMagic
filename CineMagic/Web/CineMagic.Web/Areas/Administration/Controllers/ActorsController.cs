namespace CineMagic.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CineMagic.Common;
    using CineMagic.Data;
    using CineMagic.Data.Common.Repositories;
    using CineMagic.Data.Models;
    using CineMagic.Services.Data.Contracts;
    using CineMagic.Web.ViewModels;
    using CineMagic.Web.ViewModels.Actors;
    using CineMagic.Web.ViewModels.InputModels.Administration;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ActorsController : Controller
    {
        private readonly IDeletableEntityRepository<Actor> actorsRepository;
        private readonly IActorsService actorsService;

        public ActorsController(
            IDeletableEntityRepository<Actor> actorsRepository,
            IActorsService actorsService)
        {
            this.actorsRepository = actorsRepository;
            this.actorsService = actorsService;
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
        public async Task<IActionResult> Create(ActorCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await this.actorsService.CreateAsync(inputModel);

            return this.RedirectToAction("GetAll", "Actors", new { area = "Administration" });
        }

        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return this.NotFound();
        //    }

        //    var actor = await this.actorsRepository.Actors
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (actor == null)
        //    {
        //        return this.NotFound();
        //    }

        //    return View(actor);
        //}



        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return this.NotFound();
        //    }

        //    var actor = await this.actorsRepository.Actors.FindAsync(id);
        //    if (actor == null)
        //    {
        //        return this.NotFound();
        //    }
        //    return View(actor);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Name,ProfilePicPath,Biography,Gender,Birthday,Deathday,Birthplace,Popularity,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Actor actor)
        //{
        //    if (id != actor.Id)
        //    {
        //        return this.NotFound();
        //    }

        //    if (this.ModelState.IsValid)
        //    {
        //        try
        //        {
        //            this.actorsRepository.Update(actor);
        //            await this.actorsRepository.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!this.ActorExists(actor.Id))
        //            {
        //                return this.NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return this.RedirectToAction(nameof(this.Index));
        //    }
        //    return this.View(actor);
        //}

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return this.NotFound();
        //    }

        //    var actor = await this.actorsRepository.Actors
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (actor == null)
        //    {
        //        return this.NotFound();
        //    }

        //    return View(actor);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var actor = await this.actorsRepository.Actors.FindAsync(id);
        //    this.actorsRepository.Actors.Remove(actor);
        //    await this.actorsRepository.SaveChangesAsync();
        //    return this.RedirectToAction(nameof(this.Index));
        //}

        //private bool ActorExists(int id)
        //{
        //    return this.actorsRepository.Actors.Any(e => e.Id == id);
        //}

        public async Task<IActionResult> GetAll(int page = 1)
        {
            var actors = this.actorsService.GetAllActorsAsQueryable<ActorAdministrationViewModel>();

            var paginatedList = await PaginatedList<ActorAdministrationViewModel>
                .CreateAsync(actors, page, GlobalConstants.AdministrationItemsPerPage);

            return this.View(paginatedList);
        }
    }
}
