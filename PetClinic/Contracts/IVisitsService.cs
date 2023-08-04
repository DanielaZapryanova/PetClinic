using PetClinic.Models;

namespace PetClinic.Contracts
{
    public interface IVisitsService
    {
        IList<ReasonViewModel> GetPossibleReasons();

        Task<bool> AddVisit(AddVisitViewModel addVisitViewModel);

        Task<IList<VisitViewModel>> AllVisit();
    }
}
