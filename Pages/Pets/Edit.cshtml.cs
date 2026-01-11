using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_CabinetVeterinar.Data;
using Proiect_CabinetVeterinar.Models;

namespace Proiect_CabinetVeterinar.Pages.Pets
{
    public class EditModel : PetServicesPageModel
    {
        private readonly Proiect_CabinetVeterinarContext _context;

        public EditModel(Proiect_CabinetVeterinarContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pet Pet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pet = await _context.Pet
                .Include(p => p.Owner)
                .Include(p => p.Vet)
                .Include(p => p.PetServices)
                    .ThenInclude(ps => ps.Service)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Pet == null)
            {
                return NotFound();
            }

            // Populează checkbox-urile
            PopulateAssignedServiceData(_context, Pet);

            ViewData["OwnerID"] = new SelectList(_context.Owner, "ID", "Name");
            ViewData["VetID"] = new SelectList(_context.Vet, "ID", "FullName", Pet.VetID);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedServices)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petToUpdate = await _context.Pet
                .Include(p => p.Owner)
                .Include(p => p.Vet)
                .Include(p => p.PetServices)
                    .ThenInclude(ps => ps.Service)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (petToUpdate == null)
            {
                return NotFound();
            }

            // Actualizează serviciile bifate

            if (await TryUpdateModelAsync<Pet>(
                petToUpdate,
                "Pet",
                p => p.Name, p => p.Species, p => p.Breed,
                p => p.BirthDate, p => p.OwnerID, p => p.VetID))
            {
                UpdatePetServices(_context, selectedServices, petToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Dacă apare o eroare, refacem checkbox-urile
            PopulateAssignedServiceData(_context, petToUpdate);
            ViewData["OwnerID"] = new SelectList(_context.Owner, "ID", "Name", petToUpdate.OwnerID); 
            ViewData["VetID"] = new SelectList(_context.Vet, "ID", "FullName", petToUpdate.VetID);
            return Page();
        }
    }
}
