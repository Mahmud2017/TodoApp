using Todo.DataAccess.Models;

namespace Todo.GUI.Components.Services
{
    public class TodoService : ITodoService
    {
        private readonly HttpClient _httpClient;

        public TodoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ATask>> GetAllTaskAsync()
        {
            return await _httpClient.GetFromJsonAsync<ATask[]>("todos");
        }

        public async Task<bool> UpdateTaskAsync(ATask aTask)
        {
            var result = await _httpClient.PutAsJsonAsync<ATask>("todos", aTask);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> AddTaskAsync(ATask aTask)
        {
            var result = await _httpClient.PostAsJsonAsync<ATask>("todos", aTask);

            return result.IsSuccessStatusCode;
        }
    }
}