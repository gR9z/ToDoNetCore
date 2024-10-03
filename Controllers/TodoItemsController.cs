using Microsoft.AspNetCore.Mvc;
using TodoNetCore.Data;
using TodoNetCore.Data.Repository;
using TodoNetCore.Models.DTOs;

namespace TodoNetCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController(ApplicationDbContext _context, ITodoRepository _todoRepository) : ControllerBase
{
    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
    {
        var todoItems = await _todoRepository.GetAllTasks();

        if (!todoItems.Any()) return NotFound();

        return Ok(todoItems);
    }

    // GET: api/TodoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
    {
        try
        {
            var todoItem = await _todoRepository.GetTaskById(id);
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
        var todoItems = await _todoRepository.GetTasksByStatus(isCompleted);

        if(!todoItems.Any()) return NotFound();

        return Ok(todoItems);
    }

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, TodoItemDto todoDto)
    {
        if (id != todoDto.Id)
            return BadRequest();

        try
        {
            var todoItemDto = await _todoRepository.GetTaskById(id);
            return Ok(todoItemDto);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TodoItemDto>> PostTodoItem(TodoItemDto todoDTO)
    {
        try
        {
            var createdTodoItemDto = await _todoRepository.AddTask(todoDTO);
            return CreatedAtAction(nameof(GetTodoItem), new { id = createdTodoItemDto.Id }, createdTodoItemDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
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