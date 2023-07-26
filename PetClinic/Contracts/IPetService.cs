using PetClinic.Models;

namespace PetClinic.Contracts
{
    public interface IPetService
    {
        Task<bool> AddPet(AddPetViewModel addPetViewModel);

        Task<bool> EditPet(EditPetViewModel editPetViewModel);

        Task<EditPetViewModel?> GetPet(int petId);

        Task<IList<PetViewModel>> GetAllPets();
    }
}
