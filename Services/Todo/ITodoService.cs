using TodoNetCore.Models;
using TodoNetCore.Models.DTOs;

namespace TodoNetCore.Services.Todo;

public interface ITodoService
{
    Task<IEnumerable<TodoItemDto>> FetchAllTasks();

    Task<TodoItemDto> FetchTaskById(long id);

    Task<IEnumerable<TodoItemDto>> FetchTasksByStatus(bool isCompleted);

    Task<TodoItemDto> CreateTask(TodoItemDto todoDto);

    Task<TodoItemDto> ModifyTask(TodoItemDto todoDto);

    Task RemoveTask(long id);
}