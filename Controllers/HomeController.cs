using Microsoft.AspNetCore.Mvc;
using LYDClothes.Data;
using Microsoft.EntityFrameworkCore;

namespace LYDClothes.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Products
            .Where(p => p.IsActive)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return View(products);
    }
}
