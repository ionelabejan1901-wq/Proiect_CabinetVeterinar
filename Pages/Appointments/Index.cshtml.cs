using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect_CabinetVeterinar.Data;
using Proiect_CabinetVeterinar.Models;

namespace Proiect_CabinetVeterinar.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly Proiect_CabinetVeterinarContext _context;

        public IndexModel(Proiect_CabinetVeterinarContext context)
        {
            _context = context;
        }

        public IList<Appointment> Appointment { get; set; } = new List<Appointment>();

        public async Task OnGetAsync()
        {
            Appointment = await _context.Appointment
                .Include(a => a.Owner)
                .Include(a => a.Pet)
                .Include(a => a.Service)
                .AsNoTracking()
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();
        }
    }
}
