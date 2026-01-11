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
    public class IndexModel : PageModel
    {
        private readonly Proiect_CabinetVeterinar.Data.Proiect_CabinetVeterinarContext _context;

        public IndexModel(Proiect_CabinetVeterinar.Data.Proiect_CabinetVeterinarContext context)
        {
            _context = context;
        }

        public IList<Vet> Vet { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Vet = await _context.Vet.Include(v => v.Pets).ToListAsync();
        }
    }
}
