using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using LYDClothes.Data;

var builder = WebApplication.CreateBuilder(args);

// ── Servicios ────────────────────────────────────────────
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=lyd.db"));

// Autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath         = "/Account/Login";
        options.LogoutPath        = "/Account/Logout";
        options.AccessDeniedPath  = "/Account/Login";
        options.Cookie.Name       = "LYD.Auth";
        options.Cookie.HttpOnly   = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.SlidingExpiration = true;
        options.ExpireTimeSpan    = TimeSpan.FromHours(8);
    });

builder.Services.AddAuthorization();

// ── Build ────────────────────────────────────────────────
var app = builder.Build();

// Crear la base de datos automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();   // ← primero autenticación
app.UseAuthorization();    // ← luego autorización

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
