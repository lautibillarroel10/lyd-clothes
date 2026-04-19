using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LYDClothes.Data;
using LYDClothes.Models;
using Microsoft.EntityFrameworkCore;

namespace LYDClothes.Controllers;

[Authorize]   // ← Todo el panel requiere login
public class AdminController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public AdminController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // GET: /Admin
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
        return View(products);
    }

    // GET: /Admin/Create
    public IActionResult Create() => View();

    // POST: /Admin/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product, IFormFile? image)
    {
        // Remover ImagePath de la validación porque lo manejamos aparte
        ModelState.Remove("ImagePath");

        if (ModelState.IsValid)
        {
            if (image != null && image.Length > 0)
            {
                product.ImagePath = await SaveImage(image);
            }

            product.CreatedAt = DateTime.UtcNow;
            _context.Add(product);
            await _context.SaveChangesAsync();
            TempData["Success"] = $"Producto \"{product.Name}\" creado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    // GET: /Admin/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }

    // POST: /Admin/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product, IFormFile? image)
    {
        if (id != product.Id) return NotFound();

        ModelState.Remove("ImagePath");

        if (ModelState.IsValid)
        {
            // Si subieron nueva imagen, reemplazar
            if (image != null && image.Length > 0)
            {
                product.ImagePath = await SaveImage(image);
            }
            else
            {
                // Conservar la imagen existente
                var existing = await _context.Products.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);
                product.ImagePath = existing?.ImagePath;
            }

            _context.Update(product);
            await _context.SaveChangesAsync();
            TempData["Success"] = $"Producto \"{product.Name}\" actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    // POST: /Admin/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            // Eliminar imagen del disco si existe
            if (!string.IsNullOrEmpty(product.ImagePath))
            {
                var fullPath = Path.Combine(_env.WebRootPath, product.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            TempData["Success"] = $"Producto eliminado correctamente.";
        }
        return RedirectToAction(nameof(Index));
    }

    // POST: /Admin/ToggleActive/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            product.IsActive = !product.IsActive;
            await _context.SaveChangesAsync();
            var estado = product.IsActive ? "activado" : "desactivado";
            TempData["Success"] = $"Producto {estado} correctamente.";
        }
        return RedirectToAction(nameof(Index));
    }

    // ─── Helpers ────────────────────────────────────────
    private async Task<string> SaveImage(IFormFile image)
    {
        var uploadsDir = Path.Combine(_env.WebRootPath, "uploads");
        Directory.CreateDirectory(uploadsDir);
        var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
        var filePath = Path.Combine(uploadsDir, fileName);
        using var stream = new FileStream(filePath, FileMode.Create);
        await image.CopyToAsync(stream);
        return "/uploads/" + fileName;
    }
}
