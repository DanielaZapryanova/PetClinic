using Microsoft.AspNetCore.Mvc;
using PetClinic.Contracts;
using PetClinic.Data.Models;
using PetClinic.Models;
using PetClinic.Services;

namespace PetClinic.Controllers
{
    public class PetController : Controller
    {
        IPetService petService;
        IOwnerService ownerService;

        public PetController(IPetService petService, IOwnerService ownerService)
        {
            this.petService = petService;
            this.ownerService = ownerService;
        }
        public async Task<IActionResult> AddPet()
        {
            AddPetViewModel addPetViewModel = new AddPetViewModel();
            IList<OwnerViewModel> possibleOwners = new List<OwnerViewModel>();
            possibleOwners = await ownerService.GetAllOwners();
            addPetViewModel.PossibleOwners = possibleOwners;
            return View(addPetViewModel);
        }
        public async Task<IActionResult> EditPet(int id)
        {
            var pet = await petService.GetPet(id);
            IList<OwnerViewModel> possibleOwners = new List<OwnerViewModel>();
            possibleOwners = await ownerService.GetAllOwners();
            pet.PossibleOwners = possibleOwners;
            return View(pet);
        }

        public async Task<IActionResult> DeletePet(int id)
        {
            await petService.DeletePet(id);
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> All()
        {
            IList<PetViewModel> pets = new List<PetViewModel>();
            pets = await petService.GetAllPets();
            return View(pets);
        }

        [HttpPost]
        public async Task<IActionResult> AddPet([FromForm(Name = "file")] IFormFile? file, AddPetViewModel addPetViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addPetViewModel);
            }

            await petService.AddPet(addPetViewModel);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> EditPet(EditPetViewModel editPetViewModel)
        {
            if (!ModelState.IsValid)
            {
                editPetViewModel.PossibleOwners= await GetPetOwners();
                return View(editPetViewModel);
            }

            await petService.EditPet(editPetViewModel);

            return RedirectToAction(nameof(All));
        }

        private async Task<IList<OwnerViewModel>> GetPetOwners()
        {
            return await ownerService.GetAllOwners();
        }
    }
}
