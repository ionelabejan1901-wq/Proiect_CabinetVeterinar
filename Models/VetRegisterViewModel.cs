using System.ComponentModel.DataAnnotations;

namespace Proiect_CabinetVeterinar.Models
{
    public class VetRegisterViewModel
    {
        [Required]
        [Display(Name = "Prenume")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nume")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Specializare")]
        public string Specialization { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parolă")]
        public string Password { get; set; }
    }
}
