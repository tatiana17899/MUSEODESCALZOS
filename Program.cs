using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MUSEODESCALZOS.Data;
using MUSEODESCALZOS.Models;
using Rotativa.AspNetCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("PostgressConnection") 
                      ?? throw new InvalidOperationException("Connection string 'PostgressConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configuración de Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Configuración de Stripe
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));

// Configuración de sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true; 
});

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Museo Descalzos API", Version = "v1" });
});

var app = builder.Build();

// Configuración de Rotativa
IWebHostEnvironment env = app.Environment;
if (app.Environment.IsProduction())
{
    Rotativa.AspNetCore.RotativaConfiguration.Setup(env.ContentRootPath, "Rotativa/Linux");
}
else
{
    Rotativa.AspNetCore.RotativaConfiguration.Setup(env.ContentRootPath, "Rotativa/Windows");
}

// Configuración del pipeline de HTTP
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Configuración de Swagger (disponible en todos los entornos)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Museo Descalzos API v1");
    c.RoutePrefix = ""; 
});

// Configuración de rutas
app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();