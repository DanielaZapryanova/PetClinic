using PetClinic.Models;

namespace PetClinic.Contracts
{
    public interface IOwnerService
    {
        Task<bool> AddOwner(AddOwnerViewModel addOwnerViewModel);

        Task<bool> EditOwner(EditOwnerViewModel editOwnerViewModel);

        Task<EditOwnerViewModel?> GetOwner(int ownerId);

        Task<IList<OwnerViewModel>> GetAllOwners();

        Task<bool> DeleteOwner(int id);
    }
}
