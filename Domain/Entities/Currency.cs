using System.ComponentModel.DataAnnotations;

namespace TechnicalTestApi.Domain.Entities;

public class Currency
{
    public int Id { get; set; }
    
    [Required, MaxLength(3)]
    public string Code { get; set; } = null!;  // USD, EUR, PYG, etc.
    
    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;  // Dólar Americano, Euro, Guaraní, etc.
    
    [Required, Range(0.0001, double.MaxValue)]
    public decimal RateToBase { get; set; }  // Tasa respecto a una moneda base (ej: USD)
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}