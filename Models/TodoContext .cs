using Microsoft.EntityFrameworkCore;

namespace TodoNetCore.Models;

public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
{
    public DbSet<TodoItem>? TodoItems { get; set; } = null;
}

