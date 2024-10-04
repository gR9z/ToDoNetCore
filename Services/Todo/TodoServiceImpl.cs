using TodoNetCore.Data.Repository;
using TodoNetCore.Models;
using TodoNetCore.Models.DTOs;

namespace TodoNetCore.Services.Todo;

public class TodoServiceImpl(ITodoRepository _todoRepository) : ITodoService
{
    public async Task<IEnumerable<TodoItemDto>> FetchAllTasks()
    {
        return await _todoRepository.GetAllTasks();
    }

    public async Task<TodoItemDto> FetchTaskById(long id)
    { 
        return await _todoRepository.GetTaskById(id);
    }

    public async Task<IEnumerable<TodoItemDto>> FetchTasksByStatus(bool isCompleted)
    {
        return await _todoRepository.GetTasksByStatus(isCompleted);
    }

    public async Task<TodoItemDto> CreateTask(TodoItemDto todoDto)
    {
        return await _todoRepository.AddTask(todoDto);
    }

    public async Task<TodoItemDto> ModifyTask(TodoItemDto todoDto)
    {
        return await _todoRepository.UpdateTask(todoDto);
    }

    public async Task RemoveTask(long id)
    {
        // TODO 
        throw new NotImplementedException();
    }
}