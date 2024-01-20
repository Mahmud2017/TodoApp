using Todo.DataAccess.Models;

namespace Todo.GUI.Components.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<ATask>> GetAllTaskAsync();
        Task<bool> UpdateTaskAsync(ATask aTask);
        Task<bool> AddTaskAsync(ATask aTask);
    }
}