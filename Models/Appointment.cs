using System.ComponentModel.DataAnnotations;

namespace Proiect_CabinetVeterinar.Models
{
    public class Appointment
    {
        public int ID { get; set; }
        [Display(Name = "Data programării")]
        [DataType(DataType.Date)] 
        public DateTime AppointmentDate { get; set; }
        public int? OwnerID { get; set; }
        public Owner? Owner { get; set; }
        public int? PetID { get; set; }
        public Pet? Pet { get; set; }
        public int? ServiceID { get; set; }
        public Service? Service { get; set; }
    }
}
