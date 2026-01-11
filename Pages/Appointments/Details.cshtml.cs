using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect_CabinetVeterinar.Data;
using Proiect_CabinetVeterinar.Models;

namespace Proiect_CabinetVeterinar.Pages.Appointments
{
    public class DetailsModel : PageModel
    {
        private readonly Proiect_CabinetVeterinarContext _context;

        public DetailsModel(Proiect_CabinetVeterinarContext context)
        {
            _context = context;
        }

        public Appointment Appointment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Appointment == null)
                return NotFound();

            var appointment = await _context.Appointment
                .Include(a => a.Owner)
                .Include(a => a.Pet)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (appointment == null)
                return NotFound();
            else
                Appointment = appointment;

            return Page();
        }
    }
}
