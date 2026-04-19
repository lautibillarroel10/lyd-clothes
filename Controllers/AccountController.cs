using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using LYDClothes.Models;
using System.Security.Claims;

namespace LYDClothes.Controllers;

public class AccountController : Controller
{
    private readonly IConfiguration _config;

    public AccountController(IConfiguration config)
    {
        _config = config;
    }

    // GET: /Account/Login
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        // Si ya está logueado, redirigir directo al admin
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Admin");

        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
            return View(model);

        // Leer credenciales desde appsettings.json
        var validUser = _config["AdminCredentials:Username"];
        var validPass = _config["AdminCredentials:Password"];

        if (model.Username == validUser && model.Password == validPass)
        {
            // Crear el claim de identidad
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, model.Username),
                new(ClaimTypes.Role, "Admin")
            };

            var identity  = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var authProps = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc   = model.RememberMe
                    ? DateTimeOffset.UtcNow.AddDays(30)
                    : DateTimeOffset.UtcNow.AddHours(8)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authProps);

            // Redirigir a la URL original o al panel
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Admin");
        }

        // Credenciales incorrectas
        ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
        return View(model);
    }

    // POST: /Account/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
