using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetClinic.Contracts;
using PetClinic.Data.Models;
using PetClinic.Models;
using PetClinic.Services;
using System.Data;

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

        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> AddPet()
        {
            AddPetViewModel addPetViewModel = new AddPetViewModel();
            IList<OwnerViewModel> possibleOwners = new List<OwnerViewModel>();
            possibleOwners = await ownerService.GetAllOwners();
            addPetViewModel.PossibleOwners = possibleOwners;
            addPetViewModel.DateOfBirth = DateTime.Today;
            return View(addPetViewModel);
        }

        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> EditPet(int id)
        {
            var pet = await petService.GetPet(id);
            IList<OwnerViewModel> possibleOwners = new List<OwnerViewModel>();
            possibleOwners = await ownerService.GetAllOwners();
            pet.PossibleOwners = possibleOwners;
            pet.DateOfBirth = DateTime.Today;
            return View(pet);
        }

        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> DeletePet(int id)
        {
            bool deletedPetSuccessfully = await petService.DeletePet(id);

            if (deletedPetSuccessfully)
            {
                return RedirectToAction(nameof(All));
            }

            return View("Error");
        }

        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> All()
        {
            IList<PetViewModel> pets = new List<PetViewModel>();
            pets = await petService.GetAllPets();
            return View(pets);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> AddPet([FromForm(Name = "file")] IFormFile? file, AddPetViewModel addPetViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addPetViewModel);
            }

            bool addedPetSuccessfully = await petService.AddPet(addPetViewModel);

            if (addedPetSuccessfully)
            {
                return RedirectToAction(nameof(All));
            }

            return View("Error");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> EditPet(EditPetViewModel editPetViewModel)
        {
            if (!ModelState.IsValid)
            {
                editPetViewModel.PossibleOwners= await GetPetOwners();
                return View(editPetViewModel);
            }

            bool editedPetSuccessfully = await petService.EditPet(editPetViewModel);

            if (editedPetSuccessfully)
            {
                return RedirectToAction(nameof(All));
            }

            return View("Error");
        }

        [Authorize(Roles = "Admin,Vet")]
        private async Task<IList<OwnerViewModel>> GetPetOwners()
        {
            return await ownerService.GetAllOwners();
        }
    }
}
