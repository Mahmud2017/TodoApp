using Microsoft.EntityFrameworkCore;
using Todo.DataAccess.Models;

namespace Todo.DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _taskDbContext;

        public TaskRepository(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;
        }

        public async Task<IEnumerable<ATask>> GetAllTaskAsync()
        {
            return await _taskDbContext.Tasks.ToListAsync();
        }

        public async Task<ATask> GetTaskByIdAsync(int id)
        {
            return await _taskDbContext.Tasks.FindAsync(id);
        }

        public async Task<ATask> CreateTaskAsync(ATask aTask)
        {
            _taskDbContext.Tasks.Add(aTask);
            await _taskDbContext.SaveChangesAsync();

            return aTask;
        }

        public async Task UpdateTaskAsync(ATask aTask)
        {
            _taskDbContext.Tasks.Update(aTask);
            await _taskDbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(ATask aTask)
        {
            _taskDbContext.Tasks.Remove(aTask);
            await _taskDbContext.SaveChangesAsync();
        }
    }
}
