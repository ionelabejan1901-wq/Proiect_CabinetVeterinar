using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect_CabinetVeterinar.Data;
using Proiect_CabinetVeterinar.Models;

namespace Proiect_CabinetVeterinar.Pages.Owners
{
    public class IndexModel : PageModel
    {
        private readonly Proiect_CabinetVeterinarContext _context;

        public IndexModel(Proiect_CabinetVeterinarContext context)
        {
            _context = context;
        }

        public IList<Owner> Owner { get; set; } = new List<Owner>();

        public async Task OnGetAsync()
        {
            Owner = await _context.Owner
                .AsNoTracking()
                .OrderBy(o => o.Name)
                .ToListAsync();
        }
    }
}
