namespace Proiect_CabinetVeterinar.Models
{
    public class Vet
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Specialization { get; set; }
        public ICollection<Pet>? Pets { get; set; }
    }
}
