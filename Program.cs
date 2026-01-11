using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proiect_CabinetVeterinar.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configurare baza de date
builder.Services.AddDbContext<Proiect_CabinetVeterinarContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Proiect_CabinetVeterinarContext")
        ?? throw new InvalidOperationException("Connection string 'Proiect_CabinetVeterinarContext' not found.")
    ));

// 🔹 Configurare Identity + roluri
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<Proiect_CabinetVeterinarContext>();

// 🔹 Razor Pages + protejare foldere
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Pets");
    options.Conventions.AuthorizeFolder("/Owners");
    options.Conventions.AuthorizeFolder("/Appointments");
    options.Conventions.AllowAnonymousToPage("/Vets/Index");
    options.Conventions.AllowAnonymousToPage("/Services/Details");
});

// 🔹 Adăugare suport pentru API controllers
builder.Services.AddControllers();

// 🔹 Activare CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build(); // 🔸 Acum putem folosi 'app'

// 🔹 Seed pentru roluri și cont Admin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    if (!await roleManager.RoleExistsAsync("User"))
        await roleManager.CreateAsync(new IdentityRole("User"));
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    string adminEmail = "admin@gmail.com";
    string adminPassword = "Admin123!";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var user = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}

// 🔹 Configurare pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAll"); // 🔸 CORS după routing

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers(); // 🔸 Mapăm rutele API

app.Run();
