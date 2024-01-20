using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Todo.DataAccess.Models;

namespace Todo.GUI.Components.Pages
{
    public partial class Home
    {
        private string NewTask { get; set; }
        private string FilterType { get; set; } = "All";

        private IEnumerable<ATask> TaskList = null;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            TaskList = await TodoServices.GetAllTaskAsync();
        }

        private IEnumerable<ATask> FilteredTasks =>
        FilterType switch
        {
            "Completed" => TaskList.Where(t => t.IsComplete).ToList(),
            "NotCompleted" => TaskList.Where(t => !t.IsComplete).ToList(),
            _ => TaskList
        };

        private async Task AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTask))
            {
                ATask task = new ATask();
                task.Name = NewTask;
                task.Id = 0;
                task.IsComplete = false;

                await TodoServices.AddTaskAsync(task);

                NewTask = string.Empty;

                TaskList = await TodoServices.GetAllTaskAsync();
            }
        }

        private async Task ToggleTaskStatus(ATask task)
        {
            task.IsComplete = !task.IsComplete;
            await TodoServices.UpdateTaskAsync(task);

            TaskList = await TodoServices.GetAllTaskAsync();
        }

        private string GetCardClass(ATask task) =>
            task.IsComplete ? "completed" : "not-completed";
    }
}