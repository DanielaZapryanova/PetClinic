namespace PetClinic.Models
{
    public static class VeterinarianSpecializationExtensions
    {
        public static string ToFriendlyString(this VeterinarianSpecialization vetSpecialization)
        {
            switch (vetSpecialization)
            {
                case VeterinarianSpecialization.Parasitology:
                    return "Паразитология";
                case VeterinarianSpecialization.Internals:
                    return "Вътрешни болести";
                case VeterinarianSpecialization.Emergency:
                    return "Спешна медицина";
                case VeterinarianSpecialization.Grooming:
                    return "Грижа за животните";
                default:
                    return "Обща ветеринарна медицина";
            }
        }
    }

    public enum VeterinarianSpecialization
    {
        Parasitology,
        Internals,
        Emergency,
        Grooming
    }
}
