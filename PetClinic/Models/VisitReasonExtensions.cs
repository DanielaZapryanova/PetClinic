using PetClinic.Data.Models;

namespace PetClinic.Models
{
    public static class VisitReasonExtensions
    {
        public static string ToFriendlyString(this Reason reason)
        {
            switch (reason)
            {
                case Reason.Vaccination:
                    return "Ваксинация";
                case Reason.Grooming:
                    return "Груминг";
                case Reason.BloodTest:
                    return "Кръвни тестове";
                case Reason.Examinations:
                    return "Преглед";
                default:
                    return "Първо посещение";
            }
        }
    }
}
