using System.ComponentModel.DataAnnotations;

namespace NadinSoft.Application.Contracts;

public record ProductDto
{
    public long Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; }

    [Required]
    public DateOnly ProduceDate { get; set; }

    [Required]
    [MaxLength(15)]
    public string ManufacturePhone { get; set; }

    [Required]
    [MaxLength(100)]
    public string ManufactureEmail { get; set; }

    [Required]
    public bool IsAvailable { get; set; }
}