namespace Proiect_CabinetVeterinar.Models
{
    public class Service
    {
        public int ID { get; set; }
        public string ServiceName { get; set; }
        public ICollection<PetService>? PetServices { get; set; } = new List<PetService>();
        public ICollection<Appointment>? Appointments { get; set; }


    }
}
