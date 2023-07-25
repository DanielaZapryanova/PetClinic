using PetClinic.Models;

namespace PetClinic.Contracts
{
    public interface IVetService
    {
        Task<bool> AddVet(AddVeterinarianViewModel addVetViewModel);
    }
}
