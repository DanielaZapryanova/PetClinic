using Microsoft.AspNetCore.Mvc;
using PetClinic.Contracts;
using PetClinic.Models;
using PetClinic.Services;

namespace PetClinic.Controllers
{
    public class OwnerController : Controller
    {
        IOwnerService ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            this.ownerService = ownerService;
        }
        public IActionResult AddOwner()
        {
            AddOwnerViewModel addOwnerViewModel = new AddOwnerViewModel();
            return View(addOwnerViewModel);
        }
        public async Task<IActionResult> EditOwner(int id)
        {
            var owner = await ownerService.GetOwner(id);
            return View(owner);
        }

        public async Task<IActionResult> All()
        {
            IList<OwnerViewModel> owners = new List<OwnerViewModel>();
            owners = await ownerService.GetAllOwners();
            return View(owners);
        }

        [HttpPost]
        public async Task<IActionResult> AddOwner(AddOwnerViewModel addOwnerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addOwnerViewModel);
            }

            await ownerService.AddOwner(addOwnerViewModel);

            return RedirectToAction(nameof(AddOwner));
        }

        [HttpPost]
        public async Task<IActionResult> EditPet(EditOwnerViewModel editOwnerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editOwnerViewModel);
            }

            await ownerService.EditOwner(editOwnerViewModel);

            return RedirectToAction(nameof(AddOwner));
        }
    }
}
