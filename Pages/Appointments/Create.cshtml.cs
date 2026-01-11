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

namespace Proiect_CabinetVeterinar.Pages.Appointments
{
    public class CreateModel : PageModel
    {
        private readonly Proiect_CabinetVeterinarContext _context;

        public CreateModel(Proiect_CabinetVeterinarContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["OwnerID"] = new SelectList(_context.Owner.OrderBy(o => o.Name), "ID", "Name");
            ViewData["PetID"] = new SelectList(_context.Pet.OrderBy(p => p.Name), "ID", "Name");
            ViewData["ServiceID"] = new SelectList(_context.Service.OrderBy(s => s.ServiceName), "ID", "ServiceName");
            return Page();
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["OwnerID"] = new SelectList(_context.Owner, "ID", "Name");
                ViewData["PetID"] = new SelectList(_context.Pet, "ID", "Name");
                ViewData["ServiceID"] = new SelectList(_context.Service, "ID", "ServiceName");
                return Page();
            }

            _context.Appointment.Add(Appointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
