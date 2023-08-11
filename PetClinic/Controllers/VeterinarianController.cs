using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetClinic.Contracts;
using PetClinic.Models;
using PetClinic.Services;
using System.Data;

namespace PetClinic.Controllers
{
    public class VeterinarianController : Controller
    {
        IVetService vetService;

        public VeterinarianController(IVetService vetService)
        {
            this.vetService = vetService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddVeterinarian()
        {
            AddVeterinarianViewModel addVeterinarianViewModel = new AddVeterinarianViewModel();
            List<SpecializationViewModel> specializations = new List<SpecializationViewModel>();
            foreach (VeterinarianSpecialization spec in (VeterinarianSpecialization[])Enum.GetValues(typeof(VeterinarianSpecialization)))
            {
                specializations.Add(new SpecializationViewModel
                {
                    SpecializationId = (int)spec,
                    Name = spec.ToFriendlyString(),
                });
            }
            addVeterinarianViewModel.Specializations = specializations;
            return View(addVeterinarianViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditVeterinarian(int id)
        {
            var vet = await vetService.GetVet(id);
            vet.Specializations = GetVeterinarianSpecializations();
            return View(vet);
        }

        public async Task<IActionResult> All()
        {
            IList<VeterinarianViewModel> vets = new List<VeterinarianViewModel>();
            vets = await vetService.GetAllVets();
            return View(vets);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddVeterinarian(AddVeterinarianViewModel addVetViewModel)
        {
            if (!ModelState.IsValid)
            {
                addVetViewModel.Specializations = GetVeterinarianSpecializations();
                return View(addVetViewModel);
            }
            bool addedVeterinarianSuccessfully = await vetService.AddVet(addVetViewModel);

            if (addedVeterinarianSuccessfully)
            {
                return RedirectToAction(nameof(All));
            }

            return View("Error");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditVeterinarian(EditVeterinarianViewModel editVetViewModel)
        {
            if (!ModelState.IsValid)
            {
                editVetViewModel.Specializations = GetVeterinarianSpecializations();
                return View(editVetViewModel);
            }

            bool editVeterinarianSuccessfully = await vetService.EditVet(editVetViewModel);

            if (editVeterinarianSuccessfully)
            {
                return RedirectToAction(nameof(All));
            }

            return View("Error");
        }

        [Authorize(Roles = "Admin")]
        private List<SpecializationViewModel> GetVeterinarianSpecializations()
        {
            List<SpecializationViewModel> specializations = new List<SpecializationViewModel>();
            foreach (VeterinarianSpecialization spec in (VeterinarianSpecialization[])Enum.GetValues(typeof(VeterinarianSpecialization)))
            {
                specializations.Add(new SpecializationViewModel
                {
                    SpecializationId = (int)spec,
                    Name = spec.ToFriendlyString(),
                });
            }
            return specializations;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MakeVeterinarianInactive(int id)
        {
            bool makeVeterinarianInactiveSuccessfully = await vetService.MakeVeterinarianInactive(id);

            if (makeVeterinarianInactiveSuccessfully)
            {
                return RedirectToAction(nameof(All));
            }

            return View("Error");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MakeVeterinarianActive(int id)
        {
            bool makeVeterinarianActiveSuccessfully = await vetService.MakeVeterinarianActive(id);

            if (makeVeterinarianActiveSuccessfully)
            {
                return RedirectToAction(nameof(All));
            }

            return View("Error");
        }
    }
}
