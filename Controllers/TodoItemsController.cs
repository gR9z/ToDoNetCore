using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoNetCore.Data;
using TodoNetCore.Models;
using TodoNetCore.Models.DTOs;

namespace TodoNetCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController(ApplicationDbContext _context) : ControllerBase
{
    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
    {
        return await _context.TodoItems
            .Select(item => ItemToDTO(item))
            .ToListAsync();
    }

    // GET: api/TodoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return ItemToDTO(todoItem);
    }

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, TodoItemDto todoDTO)
    {
        if (id != todoDTO.Id)
        {
            return BadRequest();
        }

        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        todoItem.Name = todoDTO.Name;
        todoItem.Description = todoDTO.Description;
        todoItem.IsComplete = todoDTO.IsComplete;
        todoItem.CreatedAt = todoDTO.CreatedAt;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoItemExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TodoItemDto>> PostTodoItem(TodoItemDto todoDTO)
    {
        var todoItem = new TodoItem
        {
            Name = todoDTO.Name,
            Description = todoDTO.Description,
            IsComplete = todoDTO.IsComplete,
            CreatedAt = todoDTO.CreatedAt
        };

        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, ItemToDTO(todoItem));
    }

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TodoItemExists(long id)
    {
        return _context.TodoItems.Any(e => e.Id == id);
    }

    private static TodoItemDto ItemToDTO(TodoItem todoItem) =>
        new TodoItemDto {
            Id = todoItem.Id,
            Name = todoItem.Name,
            Description = todoItem.Description,
            IsComplete = todoItem.IsComplete,
            CreatedAt = todoItem.CreatedAt
        };
}