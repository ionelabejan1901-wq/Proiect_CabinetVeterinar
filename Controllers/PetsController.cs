using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect_CabinetVeterinar.Data;
using Proiect_CabinetVeterinar.Models;

namespace Proiect_CabinetVeterinar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly Proiect_CabinetVeterinarContext _context;

        public PetsController(Proiect_CabinetVeterinarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPets()
        {
            return await _context.Pet.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(Pet pet)
        {
            _context.Pet.Add(pet);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPets), new { id = pet.ID }, pet);
        }
    }
}
