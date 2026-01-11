namespace Proiect_CabinetVeterinar.Models
{
    public class Pet
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public DateTime BirthDate { get; set; }
        public int? OwnerID { get; set; }

        public Owner? Owner { get; set; }
        public int? VetID { get; set; }

        public Vet? Vet { get; set; }

        public ICollection<PetService>? PetServices { get; set; } = new List<PetService>();
        public ICollection<Appointment>? Appointments { get; set; }



    }
}
