namespace Proiect_CabinetVeterinar.Models
{
    public class PetData
    {
        public IEnumerable<Pet> Pets { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<PetService> PetServices { get; set; }
    }
}
