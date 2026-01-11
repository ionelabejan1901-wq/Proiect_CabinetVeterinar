using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect_CabinetVeterinar.Data;
using Proiect_CabinetVeterinar.Models;

namespace Proiect_CabinetVeterinar.Pages.Vets
{
    public class DetailsModel : PageModel
    {
        private readonly Proiect_CabinetVeterinar.Data.Proiect_CabinetVeterinarContext _context;

        public DetailsModel(Proiect_CabinetVeterinar.Data.Proiect_CabinetVeterinarContext context)
        {
            _context = context;
        }

        public Vet Vet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vet = await _context.Vet.Include(v => v.Pets).FirstOrDefaultAsync(m => m.ID == id);
            if (vet == null)
            {
                return NotFound();
            }
            else
            {
                Vet = vet;
            }
            return Page();
        }
    }
}
