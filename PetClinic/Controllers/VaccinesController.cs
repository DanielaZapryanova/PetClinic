using Microsoft.AspNetCore.Mvc;
using PetClinic.Contracts;
using PetClinic.Models;
using PetClinic.Services;

namespace PetClinic.Controllers
{
    public class VaccinesController : Controller
    {
        IVaccineService vaccineService;

        public VaccinesController(IVaccineService vaccineService)
        {
            this.vaccineService = vaccineService;
        }

        public async Task<IActionResult> All()
        {
            IList<VaccineViewModel> vaccines = await vaccineService.GetAllVaccines();
            return View(vaccines);
        }

        public async Task<IActionResult> Add()
        {
            AddVaccineViewModel addVaccineViewModel = new AddVaccineViewModel();
            return View(addVaccineViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVaccineViewModel addVaccineViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addVaccineViewModel);
            }

            await vaccineService.AddVaccine(addVaccineViewModel);

            return RedirectToAction(nameof(All));
        }
    }
}
