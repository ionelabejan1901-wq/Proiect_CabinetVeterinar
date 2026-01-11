

using Microsoft.AspNetCore.Mvc.RazorPages;
using Proiect_CabinetVeterinar.Data;

namespace Proiect_CabinetVeterinar.Models
{
    public class PetServicesPageModel : PageModel
    {
        public List<AssignedServiceData> AssignedServicesDataList { get; set; } = new();

        public void PopulateAssignedServiceData(Proiect_CabinetVeterinarContext context, Pet pet)
        {
            var allServices = context.Service;
            var petServices = new HashSet<int>(pet.PetServices.Select(s => s.ServiceID));

            AssignedServicesDataList = new List<AssignedServiceData>();

            foreach (var service in allServices)
            {
                AssignedServicesDataList.Add(new AssignedServiceData
                {
                    ServiceID = service.ID,
                    Name = service.ServiceName,
                    Assigned = petServices.Contains(service.ID)
                });
            }
        }

        public void UpdatePetServices(Proiect_CabinetVeterinarContext context,
                               string[] selectedServices, Pet petToUpdate)
        {
            if (selectedServices == null)
            {
                petToUpdate.PetServices = new List<PetService>();
                return;
            }

            var selectedHS = new HashSet<string>(selectedServices);
            var petServices = new HashSet<int>(petToUpdate.PetServices.Select(s => s.ServiceID));

            foreach (var service in context.Service)
            {
                if (selectedHS.Contains(service.ID.ToString()))
                {
                    if (!petServices.Contains(service.ID))
                    {
                        petToUpdate.PetServices.Add(new PetService
                        {
                            PetID = petToUpdate.ID,
                            ServiceID = service.ID
                        });
                    }
                }
                else
                {
                    if (petServices.Contains(service.ID))
                    {
                        PetService serviceToRemove =
                            petToUpdate.PetServices.SingleOrDefault(s => s.ServiceID == service.ID);
                        context.Remove(serviceToRemove);
                    }
                }
            }
        }

    }
}
