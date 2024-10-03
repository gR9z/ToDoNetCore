using System.ComponentModel.DataAnnotations;

namespace TodoNetCore.Services.Auth.Models;

public class Login
{
    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}