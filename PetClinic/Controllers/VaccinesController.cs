using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetClinic.Contracts;
using PetClinic.Models;
using PetClinic.Services;
using System.Data;

namespace PetClinic.Controllers
{
    public class VaccinesController : Controller
    {
        IVaccineService vaccineService;

        public VaccinesController(IVaccineService vaccineService)
        {
            this.vaccineService = vaccineService;
        }

        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> All()
        {
            IList<VaccineViewModel> vaccines = await vaccineService.GetAllVaccines();
            return View(vaccines);
        }

        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> Add()
        {
            AddVaccineViewModel addVaccineViewModel = new AddVaccineViewModel();
            addVaccineViewModel.DateOfExpiry = DateTime.Today;
            return View(addVaccineViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> Add(AddVaccineViewModel addVaccineViewModel)
        {
            if ((addVaccineViewModel.DateOfExpiry - DateTime.Now).TotalDays <= 0)
            {
                ModelState.AddModelError(nameof(addVaccineViewModel.DateOfExpiry), "Не може да добавите ваксина с минал срок на годност.");
            }

            if (addVaccineViewModel.NumberInStock>=100)
            {
                ModelState.AddModelError(nameof(addVaccineViewModel.NumberInStock), "Не може да добавите ваксина, защото няма място в склада.");
            }

            if (!ModelState.IsValid)
            {
                return View(addVaccineViewModel);
            }

            bool addedVaccineSuccessfully = await vaccineService.AddVaccine(addVaccineViewModel);

            if (addedVaccineSuccessfully)
            {
                return RedirectToAction(nameof(All));
            }

            return View("Error");
        }
    }
}
