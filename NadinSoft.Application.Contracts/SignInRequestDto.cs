using System.ComponentModel.DataAnnotations;

namespace NadinSoft.Application.Contracts;

public record SignInRequestDto
{
    [Required(ErrorMessage = "UserName is required")]
    [MaxLength(100)]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}