using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect_CabinetVeterinar.Data;
using Proiect_CabinetVeterinar.Models;
using Proiect_CabinetVeterinar.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_CabinetVeterinar.Pages.Services
{
    public class IndexModel : PageModel
    {
        private readonly Proiect_CabinetVeterinarContext _context;

        public IndexModel(Proiect_CabinetVeterinarContext context)
        {
            _context = context;
        }

        public ServiceIndexData ServiceData { get; set; }
        public int ServiceID { get; set; }

        public async Task OnGetAsync(int? id)
        {
            ServiceData = new ServiceIndexData();

            ServiceData.Services = await _context.Service
                .Include(s => s.PetServices)
                    .ThenInclude(ps => ps.Pet)
                        .ThenInclude(p => p.Owner)
                .Include(s => s.PetServices)
                    .ThenInclude(ps => ps.Pet)
                        .ThenInclude(p => p.Vet)
                .OrderBy(s => s.ServiceName)
                .ToListAsync();

            if (id != null)
            {
                ServiceID = id.Value;

                var service = ServiceData.Services
                    .Where(s => s.ID == id.Value)
                    .Single();

                ServiceData.Pets = service.PetServices
                    .Select(ps => ps.Pet);
            }
        }
    }
}
