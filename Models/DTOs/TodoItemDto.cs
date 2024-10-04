using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoNetCore.Models.DTOs;

public class TodoItemDto
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Le nom est requis.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Le nom doit contenir entre {2} et {1} caractères.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "La description est requise.")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "La description doit contenir entre {2} et {1} caractères.")]
    public required string Description { get; set; }

    public bool? IsComplete { get; set; } = false;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}