using Microsoft.AspNetCore.Mvc;
using PetClinic.Contracts;
using PetClinic.Models;

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

        //public async Task<IActionResult> GetAllVisits(int animalId)
        //{

        //}

        [HttpPost]
        public async Task<IActionResult> AddVisit(AddVisitViewModel addVisitViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addVisitViewModel);
            }

            await visitsService.AddVisit(addVisitViewModel);

            return RedirectToAction(nameof(AddVisit));
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
