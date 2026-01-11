using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Proiect_CabinetVeterinar.Data;
using Proiect_CabinetVeterinar.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;


namespace Proiect_CabinetVeterinar.Pages.Vets
{
    [Authorize(Roles = "Admin")]
    public class CreateAdminVetModel : PageModel
    {
        private readonly Proiect_CabinetVeterinarContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateAdminVetModel(Proiect_CabinetVeterinarContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public VetRegisterViewModel Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = new IdentityUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, Input.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return Page();
            }

            await _userManager.AddToRoleAsync(user, "Admin");

            var vet = new Vet
            {
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                Specialization = Input.Specialization
            };

            _context.Vet.Add(vet);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Veterinarul a fost creat cu succes!";
            return RedirectToPage("/Vets/Index");
        }

        public class VetRegisterViewModel
        {
            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            public string Specialization { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
