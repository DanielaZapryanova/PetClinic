using Microsoft.AspNetCore.Mvc;
using PetClinic.Contracts;
using PetClinic.Models;

namespace PetClinic.Controllers
{
    public class VeterinarianController : Controller
    {
        IVetService vetService;

        public VeterinarianController(IVetService vetService)
        {
            this.vetService = vetService;
        }

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
        public async Task<IActionResult> AddVeterinarian(AddVeterinarianViewModel addVetViewModel)
        {
            if (!ModelState.IsValid)
            {
                addVetViewModel.Specializations = GetVeterinarianSpecializations();
                return View(addVetViewModel);
            }

            await vetService.AddVet(addVetViewModel);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> EditVeterinarian(EditVeterinarianViewModel editVetViewModel)
        {
            if (!ModelState.IsValid)
            {
                editVetViewModel.Specializations = GetVeterinarianSpecializations();
                return View(editVetViewModel);
            }

            await vetService.EditVet(editVetViewModel);

            return RedirectToAction(nameof(All));
        }

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

        public async Task<IActionResult> MakeVeterinarianInactive(int id)
        {
            await vetService.MakeVeterinarianInactive(id);
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> MakeVeterinarianActive(int id)
        {
            await vetService.MakeVeterinarianActive(id);
            return RedirectToAction(nameof(All));
        }
    }
}
