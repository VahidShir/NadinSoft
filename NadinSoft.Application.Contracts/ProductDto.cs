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
    [RegularExpression("^0[0-9]{2,}[0-9]{7,}$")]
    //Regular expression for Iran Landline phones
    //https://www.datisnetwork.com/phone-number-regex.html
    public string ManufacturePhone { get; set; }

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string ManufactureEmail { get; set; }

    [Required]
    public bool IsAvailable { get; set; }

    public string CreatedBy { get; set; }
}