using System.ComponentModel.DataAnnotations;

namespace LYDClothes.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [Display(Name = "Nombre")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Descripción")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Display(Name = "Precio")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Display(Name = "Categoría")]
    public string Category { get; set; } = string.Empty;

    [Display(Name = "Imagen")]
    public string? ImagePath { get; set; }

    [Display(Name = "Activo")]
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
