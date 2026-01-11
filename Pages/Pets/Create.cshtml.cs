using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proiect_CabinetVeterinar.Data;
using Proiect_CabinetVeterinar.Models;

namespace Proiect_CabinetVeterinar.Pages.Pets
{
    public class CreateModel : PetServicesPageModel
    {
        private readonly Proiect_CabinetVeterinar.Data.Proiect_CabinetVeterinarContext _context;

        public CreateModel(Proiect_CabinetVeterinar.Data.Proiect_CabinetVeterinarContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var pet = new Pet();
            pet.PetServices = new List<PetService>();

            PopulateAssignedServiceData(_context, pet);

            ViewData["OwnerID"] = new SelectList(_context.Owner, "ID", "Name");
            ViewData["VetID"] = new SelectList(_context.Vet, "ID", "FullName");

            return Page();
        }


        [BindProperty]
        public Pet Pet { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedServices)
        {
            var newPet = new Pet();

            if (selectedServices != null)
            {
                newPet.PetServices = new List<PetService>();

                foreach (var service in selectedServices)
                {
                    newPet.PetServices.Add(new PetService
                    {
                        ServiceID = int.Parse(service)
                    });
                }
            }

            Pet.PetServices = newPet.PetServices;

            _context.Pet.Add(Pet);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
