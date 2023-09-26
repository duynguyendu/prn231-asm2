using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Asm2.eBookStore.Api.ApiModel;

public class LoginCredential
{
    [Required]
    public string? Email { get; set; }

    [Required]
    [PasswordPropertyText]
    public string? Password { get; set; }
}
