using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetClinic.Contracts;
using PetClinic.Models;
using PetClinic.Services;
using System.Data;
using System.Security.Policy;

namespace PetClinic.Controllers
{
    public class OwnerController : Controller
    {
        IOwnerService ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            this.ownerService = ownerService;
        }

        [Authorize(Roles = "Admin,Vet")]
        public IActionResult AddOwner()
        {
            AddOwnerViewModel addOwnerViewModel = new AddOwnerViewModel();
            return View("AddOwner", addOwnerViewModel);
        }

        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> EditOwner(int id)
        {
            var owner = await ownerService.GetOwner(id);
            return View("EditOwner", owner);
        }

        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> All()
        {
            IList<OwnerViewModel> owners = new List<OwnerViewModel>();
            owners = await ownerService.GetAllOwners();
            return View("All", owners);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> AddOwner(AddOwnerViewModel addOwnerViewModel)
        {
            if (addOwnerViewModel.Age <= 17)
            {
                ModelState.AddModelError(nameof(addOwnerViewModel.Age), "Не може да се създаде непълнолетен собственик.");
            }
            if (!ModelState.IsValid)
            {
                return View("AddOwner", addOwnerViewModel);
            }

            bool addedOwnerSuccessfully = await ownerService.AddOwner(addOwnerViewModel);

            if (addedOwnerSuccessfully)
            {
                return RedirectToAction(nameof(All));
            }

            return View("Error");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> EditOwner(EditOwnerViewModel editOwnerViewModel)
        {
            if (editOwnerViewModel.Age <= 17)
            {
                ModelState.AddModelError(nameof(editOwnerViewModel.Age), "Не може да редактирате годините, защото собственика е непълнолетен.");
            }
            if (!ModelState.IsValid)
            {
                return View("EditOwner", editOwnerViewModel);
            }
            bool editedOwnerSuccessfully = await ownerService.EditOwner(editOwnerViewModel);

            if (editedOwnerSuccessfully)
            {
                return RedirectToAction(nameof(All));
            }

            return View("Error");
        }

        [Authorize(Roles = "Admin,Vet")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            bool deletedOwnerSuccessfully = await ownerService.DeleteOwner(id);

            if (deletedOwnerSuccessfully)
            {
                return RedirectToAction(nameof(All));
            }

            return View("Error");
        }
    }
}
