using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_CabinetVeterinar.Data;
using Proiect_CabinetVeterinar.Models;

namespace Proiect_CabinetVeterinar.Pages.Appointments
{
    public class EditModel : PageModel
    {
        private readonly Proiect_CabinetVeterinarContext _context;

        public EditModel(Proiect_CabinetVeterinarContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Appointment == null)
                return NotFound();

            Appointment = await _context.Appointment
                .Include(a => a.Owner)
                .Include(a => a.Pet)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Appointment == null)
                return NotFound();

            ViewData["OwnerID"] = new SelectList(_context.Owner, "ID", "Name");
            ViewData["PetID"] = new SelectList(_context.Pet, "ID", "Name");
            ViewData["ServiceID"] = new SelectList(_context.Service, "ID", "ServiceName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Attach(Appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Appointment.Any(e => e.ID == Appointment.ID))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
