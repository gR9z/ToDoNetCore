using TodoNetCore.Models.DTOs;

namespace TodoNetCore.Data.Repository;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItemDto>> GetAllTasks();

    Task<TodoItemDto> GetTaskById(long id);

    Task<IEnumerable<TodoItemDto>> GetTasksByStatus(bool isCompleted);

    Task<TodoItemDto> AddTask(TodoItemDto todoDto);

    Task<TodoItemDto> UpdateTask(TodoItemDto todoDto);

    Task DeleteTodoItem(long id);
}