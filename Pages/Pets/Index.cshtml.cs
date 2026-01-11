using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect_CabinetVeterinar.Data;
using Proiect_CabinetVeterinar.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Proiect_CabinetVeterinar.Pages.Pets
{
    public class IndexModel : PageModel
    {
        private readonly Proiect_CabinetVeterinarContext _context;

        public IndexModel(Proiect_CabinetVeterinarContext context)
        {
            _context = context;
        }

        public PetData PetD { get; set; }
        public int PetID { get; set; }
        public int ServiceID { get; set; }

        public SelectList Vets { get; set; }
        public int? VetID { get; set; }

        public async Task OnGetAsync(int? id, int? serviceID, int? vetId)
        {
            VetID = vetId;

            // Dropdown pentru medici
            Vets = new SelectList(_context.Vet, "ID", "FullName");

            PetD = new PetData();

            // Include toate relațiile necesare
            var pets = _context.Pet
                .Include(p => p.Owner)
                .Include(p => p.Vet)
                .Include(p => p.PetServices)
                    .ThenInclude(ps => ps.Service)
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .AsQueryable();

            // Filtrare după medic (opțional)
            if (vetId != null)
            {
                pets = pets.Where(p => p.VetID == vetId);
            }

            PetD.Pets = await pets.ToListAsync();

            // Dacă un animal este selectat, afișăm serviciile lui
            if (id != null)
            {
                PetID = id.Value;
                Pet pet = PetD.Pets
                    .Where(p => p.ID == id.Value)
                    .Single();

                PetD.Services = pet.PetServices
                    .Select(s => s.Service);
            }
        }
    }
}
