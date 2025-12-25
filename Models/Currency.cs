using System.ComponentModel.DataAnnotations;

namespace TechnicalTestApi.Models;

public class Currency
{
    public int Id { get; set; }

    [Required]
    public string Code { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public decimal RateToBase { get; set; }
}
