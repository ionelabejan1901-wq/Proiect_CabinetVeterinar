using System.ComponentModel.DataAnnotations;

namespace Proiect_CabinetVeterinar.Models
{
    public class Owner
    {
        public int ID { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage = "Prenumele trebuie sa inceapa cu majuscula (ex. Ana sau Ana Maria sau AnaMaria")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(10)] 
        public string Phone { get; set; } 
        public string Email { get; set; }
        public ICollection<Pet>? Pets { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }


    }
}
