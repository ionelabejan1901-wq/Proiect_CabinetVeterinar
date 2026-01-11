using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect_CabinetVeterinar.Data;
using Proiect_CabinetVeterinar.Models;

namespace Proiect_CabinetVeterinar.Pages.Owners
{
    public class DeleteModel : PageModel
    {
        private readonly Proiect_CabinetVeterinar.Data.Proiect_CabinetVeterinarContext _context;

        public DeleteModel(Proiect_CabinetVeterinar.Data.Proiect_CabinetVeterinarContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Owner Owner { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owner.FirstOrDefaultAsync(m => m.ID == id);

            if (owner == null)
            {
                return NotFound();
            }
            else
            {
                Owner = owner;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owner.FindAsync(id);
            if (owner != null)
            {
                Owner = owner;
                _context.Owner.Remove(Owner);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
