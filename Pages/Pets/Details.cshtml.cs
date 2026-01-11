using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect_CabinetVeterinar.Data;
using Proiect_CabinetVeterinar.Models;

namespace Proiect_CabinetVeterinar.Pages.Pets
{
    public class DetailsModel : PageModel
    {
        private readonly Proiect_CabinetVeterinarContext _context;

        public DetailsModel(Proiect_CabinetVeterinarContext context)
        {
            _context = context;
        }

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

            return Page();
        }
    }
}
