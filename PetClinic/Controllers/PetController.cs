using Microsoft.AspNetCore.Mvc;
using PetClinic.Contracts;
using PetClinic.Models;
using PetClinic.Services;

namespace PetClinic.Controllers
{
    public class PetController : Controller
    {
        IPetService petService;

        public PetController(IPetService petService)
        {
            this.petService = petService;
        }
        public IActionResult AddPet()
        {
            AddPetViewModel addPetViewModel = new AddPetViewModel();
            return View (addPetViewModel);
        }
        public async Task<IActionResult> EditPet(int id)
        {
            var pet = await petService.GetPet(id);
            return View(pet);
        }

        public async Task<IActionResult> All()
        {
            IList<PetViewModel> pets = new List<PetViewModel>();
            pets = await petService.GetAllPets();
            return View(pets);
        }

        [HttpPost]
        public async Task<IActionResult> AddPet(AddPetViewModel addPetViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addPetViewModel);
            }

            await petService.AddPet(addPetViewModel);

            return RedirectToAction(nameof(AddPet));
        }

        [HttpPost]
        public async Task<IActionResult> EditPet(EditPetViewModel editPetViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editPetViewModel);
            }

            await petService.EditPet(editPetViewModel);

            return RedirectToAction(nameof(AddPet));
        }
    }
}
