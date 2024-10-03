using Microsoft.EntityFrameworkCore;
using TodoNetCore.Models;

namespace TodoNetCore.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<TodoItem>? TodoItems { get; set; } = null;
}