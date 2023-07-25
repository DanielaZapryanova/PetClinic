using Microsoft.AspNetCore.Mvc;
using PetClinic.Models;

namespace PetClinic.Controllers
{
    public class VeterinarianController : Controller
    {
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
    }
}
