using System.ComponentModel.DataAnnotations;

namespace TodoNetCore.Models;

public class TodoItem
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Le nom est requis.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Le nom doit contenir entre {2} et {1} caractères.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "La description est requise.")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "La description doit contenir entre {2} et {1} caractères.")]
    public string Description { get; set; }

    public bool? IsComplete { get; set; } = false;

    public string? Secret { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}