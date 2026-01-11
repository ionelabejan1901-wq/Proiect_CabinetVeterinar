using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proiect_CabinetVeterinar.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Proiect_CabinetVeterinar.Data
{
    public class Proiect_CabinetVeterinarContext : IdentityDbContext
    {
        public Proiect_CabinetVeterinarContext (DbContextOptions<Proiect_CabinetVeterinarContext> options)
            : base(options)
        {
        }

        public DbSet<Proiect_CabinetVeterinar.Models.Pet> Pet { get; set; } = default!;
        public DbSet<Proiect_CabinetVeterinar.Models.Owner> Owner { get; set; } = default!;
        public DbSet<Proiect_CabinetVeterinar.Models.Vet> Vet { get; set; } = default!;
        public DbSet<Proiect_CabinetVeterinar.Models.Service> Service { get; set; } = default!;
        public DbSet<Proiect_CabinetVeterinar.Models.Appointment> Appointment { get; set; } = default!;
        
    }
}
