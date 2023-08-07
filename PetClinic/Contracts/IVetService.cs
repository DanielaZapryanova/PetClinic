using Microsoft.AspNetCore.Mvc;
using PetClinic.Models;

namespace PetClinic.Contracts
{
    public interface IVetService
    {
        Task<bool> AddVet(AddVeterinarianViewModel addVetViewModel);

        Task<bool> EditVet(EditVeterinarianViewModel editVetViewModel);

        Task<EditVeterinarianViewModel?> GetVet(int vetId);

        Task<bool> MakeVeterinarianInactive(int vetId);

        Task<IList<VeterinarianViewModel>> GetAllVets();

        Task<IList<VeterinarianViewModel>> GetAllActiveVets();

        Task<bool> MakeVeterinarianActive(int id);
    }
}
