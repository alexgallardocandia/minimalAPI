using System.ComponentModel.DataAnnotations;

namespace TechnicalTestApi.Domain.Entities;

public class Currency
{
    public int Id { get; set; }

    [Required, MaxLength(3)]
    public string Code { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required, Range(0.0001, double.MaxValue)]
    public decimal RateToBase { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
