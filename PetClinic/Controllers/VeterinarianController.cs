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

        [HttpPost]
        public async Task<IActionResult> AddVeterinarian(AddVeterinarianViewModel addVetViewModel)
        {
            if (!ModelState.IsValid)
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
                addVetViewModel.Specializations = specializations;
                return View(addVetViewModel);
            }

            await vetService.AddVet(addVetViewModel);

            return RedirectToAction(nameof(AddVeterinarian));
        }
    }
}
