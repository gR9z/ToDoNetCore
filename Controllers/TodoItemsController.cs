using Microsoft.AspNetCore.Mvc;
using TodoNetCore.Data;
using TodoNetCore.Data.Repository;
using TodoNetCore.Models.DTOs;
using TodoNetCore.Services.Todo;

namespace TodoNetCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController(ApplicationDbContext _context, ITodoRepository _todoRepository, ITodoService _todoService) : ControllerBase
{
    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
    {
        var todoItems = await _todoService.FetchAllTasks();

        if (!todoItems.Any()) return NotFound();

        return Ok(todoItems);
    }

    // GET: api/TodoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
    {
        try
        {
            var todoItem = await _todoService.FetchTaskById(id);
            return Ok(todoItem);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { ex.Message });
        }
    }

    // GET: api/TodoItems/status
    [HttpGet("status/{isCompleted}")]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoByStatus(bool isCompleted) {
        var todoItems = await _todoService.FetchTasksByStatus(isCompleted);

        if(!todoItems.Any()) return NotFound();

        return Ok(todoItems);
    }

    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TodoItemDto>> PostTodoItem(TodoItemDto todoDTO)
    {
        try
        {
            var createdTodoItemDto = await _todoService.CreateTask(todoDTO);
            return CreatedAtAction(nameof(GetTodoItem), new { id = createdTodoItemDto.Id }, createdTodoItemDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
    
    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<ActionResult<TodoItemDto>> PutTodoItem(long id, TodoItemDto todoDto)
    {
        if (id != todoDto.Id)
            return BadRequest();

        try
        {
            return await _todoService.ModifyTask(todoDto);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        try
        {
            await _todoRepository.DeleteTodoItem(id);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { ex.Message });
        }
    }
}