using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect_CabinetVeterinar.Data;
using Proiect_CabinetVeterinar.Models;
using Microsoft.AspNetCore.Authorization;


namespace Proiect_CabinetVeterinar.Pages.Vets
{
    public class DeleteModel : PageModel
    {
        private readonly Proiect_CabinetVeterinar.Data.Proiect_CabinetVeterinarContext _context;

        public DeleteModel(Proiect_CabinetVeterinar.Data.Proiect_CabinetVeterinarContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vet = await _context.Vet.FindAsync(id);
            if (vet != null)
            {
                Vet = vet;
                _context.Vet.Remove(Vet);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
