﻿namespace TodoNetCore.Models;

public class TodoItem
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public bool? IsComplete { get; set; } = false;

    public string? Secret { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;
}