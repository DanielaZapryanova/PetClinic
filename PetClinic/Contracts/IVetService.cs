using PetClinic.Models;

namespace PetClinic.Contracts
{
    public interface IVetService
    {
        Task<bool> AddVet(AddVeterinarianViewModel addVetViewModel);

        Task<bool> EditVet(EditVeterinarianViewModel editVetViewModel);

        Task<EditVeterinarianViewModel?> GetVet(int vetId);
    }
}
