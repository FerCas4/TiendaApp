using Microsoft.EntityFrameworkCore;
using TiendaApp.Data; // Esto soluciona el error de ApplicationDbContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// CONFIGURACIÓN DE POSTGRESQL (Puesta antes de builder.Build())
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// En .NET 9+, MapStaticAssets reemplaza en parte a UseStaticFiles, pero para asegurar compatibilidad:
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Productos}/{action=Index}/{id?}"); // Redirige directamente a tu CRUD de Productos

app.Run();