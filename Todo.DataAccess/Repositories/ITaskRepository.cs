using Todo.DataAccess.Models;

namespace Todo.DataAccess.Repositories
{
    public interface ITaskRepository
    {
        Task<ATask> CreateTaskAsync(ATask aTask);
        Task DeleteTaskAsync(ATask aTask);
        Task<IEnumerable<ATask>> GetAllTaskAsync();
        Task<ATask> GetTaskByIdAsync(int id);
        Task UpdateTaskAsync(ATask aTask);
    }
}