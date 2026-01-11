namespace Proiect_CabinetVeterinar.Models
{
    public class PetService
    {
        
            public int ID { get; set; }

            public int PetID { get; set; }
            public Pet Pet { get; set; }

            public int ServiceID { get; set; }
            public Service Service { get; set; }
    

    }
}
