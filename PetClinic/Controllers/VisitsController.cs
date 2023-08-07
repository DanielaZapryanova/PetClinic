using Microsoft.AspNetCore.Mvc;
using PetClinic.Contracts;
using PetClinic.Models;
using PetClinic.Services;

namespace PetClinic.Controllers
{
    public class VisitsController : Controller
    {
        IVisitsService visitsService;
        IPetService petService;
        IVetService vetService;

        public VisitsController(IVisitsService visitsService, IPetService petService, IVetService vetService)
        {
            this.visitsService = visitsService;
            this.petService = petService;
            this.vetService = vetService;
        }

        public async Task<IActionResult> AllVisit()
        {
            IList<VisitViewModel> visits = await visitsService.AllVisit();
            return View(visits);
        }
        public async Task<IActionResult> Visits(int id)
        {
            IList<VisitViewModel> visits = await visitsService.Visits(id);
            return View(visits);
        }

        [HttpPost]
        public async Task<IActionResult> AddVisit(AddVisitViewModel addVisitViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addVisitViewModel);
            }

            await visitsService.AddVisit(addVisitViewModel);

            return RedirectToAction(nameof(AllVisit));
        }

        public async Task<IActionResult> AddVisit()
        {
            AddVisitViewModel addVisitViewModel = new AddVisitViewModel();
            addVisitViewModel.PossibleReasons = visitsService.GetPossibleReasons();
            addVisitViewModel.PossibleVets = await vetService.GetAllVets();
            addVisitViewModel.PossiblePets = await petService.GetAllPets();
            return View(addVisitViewModel);
        }
    }
}
