using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> AllVisit()
        {
            IList<VisitViewModel> visits = await visitsService.AllVisit();
            return View("AllVisit", visits);
        }

        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> Visits(int id)
        {
            IList<VisitViewModel> visits = await visitsService.Visits(id);
            return View("Visits", visits);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> AddVisit(AddVisitViewModel addVisitViewModel)
        {
            if ((addVisitViewModel.Date - DateTime.Now).TotalDays > 3)
            {
                ModelState.AddModelError(nameof(addVisitViewModel.Date), "Не може да добавите посещение, случило се преди повече от 3 дни.");
            }

            if (!ModelState.IsValid)
            {
                return View("AddVisit", addVisitViewModel);
            }

            bool addedVisitSuccessfully = await visitsService.AddVisit(addVisitViewModel);

            if (addedVisitSuccessfully)
            {
                return RedirectToAction(nameof(AllVisit));
            }

            return View("Error");
        }

        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> AddVisit()
        {
            AddVisitViewModel addVisitViewModel = new AddVisitViewModel();
            addVisitViewModel.PossibleReasons = visitsService.GetPossibleReasons();
            addVisitViewModel.PossibleVets = await vetService.GetAllActiveVets();
            addVisitViewModel.PossiblePets = await petService.GetAllPets();
            addVisitViewModel.Date = DateTime.Today;
            return View("AddVisit", addVisitViewModel);
        }

        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> AddVaccination()
        {
            AddVaccinationViewModel addVaccinationViewModel = new AddVaccinationViewModel();
            addVaccinationViewModel.PossibleVaccines = await visitsService.GetPossibleVaccines();
            addVaccinationViewModel.PossibleVets = await vetService.GetAllActiveVets();
            addVaccinationViewModel.PossiblePets = await petService.GetAllPets();
            addVaccinationViewModel.Date = DateTime.Today;
            return View("AddVaccination", addVaccinationViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> AddVaccination(AddVaccinationViewModel addVaccinationViewModel)
        {
            if (addVaccinationViewModel.Date.CompareTo(DateTime.Now) > 0)
            {
                ModelState.AddModelError(nameof(addVaccinationViewModel.Date), "Не може да ваксинирате в бъдещето.");
            }

            if ((addVaccinationViewModel.Date - DateTime.Now).TotalDays > 3)
            {
                ModelState.AddModelError(nameof(addVaccinationViewModel.Date), "Не може да добавите ваксинация, случила се преди повече от 3 дни.");
            }

            if (!ModelState.IsValid)
            {
                addVaccinationViewModel.PossibleVaccines = await visitsService.GetPossibleVaccines();
                addVaccinationViewModel.PossibleVets = await vetService.GetAllActiveVets();
                addVaccinationViewModel.PossiblePets = await petService.GetAllPets();
                return View(addVaccinationViewModel);
            }

            bool addedVaccinationSuccessfully = await visitsService.AddVaccination(addVaccinationViewModel);

            if (addedVaccinationSuccessfully)
            {
                return RedirectToAction(nameof(AllVisit));
            }

            return View("Error");
        }
    }
}
