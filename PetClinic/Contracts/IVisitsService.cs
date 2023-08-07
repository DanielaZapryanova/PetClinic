using PetClinic.Models;

namespace PetClinic.Contracts
{
    public interface IVisitsService
    {
        IList<ReasonViewModel> GetPossibleReasons();

        Task<IList<VaccineViewModel>> GetPossibleVaccines();

        Task<bool> AddVisit(AddVisitViewModel addVisitViewModel);

        Task<bool> AddVaccination(AddVaccinationViewModel addVaccinationViewModel);

        Task<IList<VisitViewModel>> AllVisit();

        Task<IList<VisitViewModel>> Visits(int id);
    }
}
