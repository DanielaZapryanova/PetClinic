using PetClinic.Models;

namespace PetClinic.Contracts
{
    public interface IVaccineService
    {
        Task<IList<VaccineViewModel>> GetAllVaccines();

        Task<bool> AddVaccine(AddVaccineViewModel addVaccineViewModel);
    }
}
