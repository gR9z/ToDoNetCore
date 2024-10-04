using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TodoNetCore.Models;
using TodoNetCore.Models.DTOs;

namespace TodoNetCore.Data.Repository.Impl;

public class TodoRepositoryImpl(ApplicationDbContext _context, ILogger<TodoRepositoryImpl> _logger) : ITodoRepository
{
    public async Task<IEnumerable<TodoItemDto>> GetAllTasks()
    {
        return await _context.TodoItems
            .Select(item => ItemToDTO(item))
            .ToListAsync();
    }

    public async Task<TodoItemDto> GetTaskById(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);

        if (todoItem == null) throw new KeyNotFoundException($"L'élément avec l'ID {id} n'a pas été trouvé.");

        return ItemToDTO(todoItem);
    }

    public async Task<IEnumerable<TodoItemDto>> GetTasksByStatus(bool isCompleted) {
        return await _context.TodoItems
            .Where(item => item.IsComplete == isCompleted)
            .Select(item => ItemToDTO(item))
            .ToListAsync();
    }

    public async Task<TodoItemDto> AddTask(TodoItemDto todoDto)
    {
        var todoItem = new TodoItem
        {
            Name = todoDto.Name,
            Description = todoDto.Description,
            IsComplete = todoDto.IsComplete,
        };
        
        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();

        return ItemToDTO(todoItem);
    }

    public async Task<TodoItemDto> UpdateTask(TodoItemDto todoDto)
    {
        var id = todoDto.Id;
        var todoItem = await _context.TodoItems.FindAsync(id);

        if (todoItem == null) throw new KeyNotFoundException($"L'élément avec l'ID {id} n'a pas été trouvé.");

        todoItem.Name = todoDto.Name;
        todoItem.Description = todoDto.Description;
        todoItem.IsComplete = todoDto.IsComplete;

        try
        {
            await _context.SaveChangesAsync();
            return ItemToDTO(todoItem);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoItemExists(id)) throw new KeyNotFoundException($"L'élément avec l'ID {id} n'existe plus.");

            throw;
        }
    }

    public async Task DeleteTodoItem(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);

        if (todoItem == null) throw new KeyNotFoundException($"L'élément avec l'ID {id} n'a pas été trouvé.");

        try
        {
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoItemExists(id)) throw new KeyNotFoundException($"L'élément avec l'ID {id} n'existe plus.");

            throw;
        }
    }

    private bool TodoItemExists(long id)
    {
        return _context.TodoItems.Any(e => e.Id == id);
    }

    private static TodoItemDto ItemToDTO(TodoItem todoItem)
    {
        return new TodoItemDto()
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            Description = todoItem.Description,
            IsComplete = todoItem.IsComplete,
            CreatedAt = todoItem.CreatedAt
        };
    }
}