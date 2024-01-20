using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.DataAccess.Models;
using Todo.DataAccess.Repositories;

namespace Todo.API.Controllers
{
    [Route("todos")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepo;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITaskRepository taskRepo, ILogger<TasksController> logger)
        {
            _taskRepo = taskRepo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(ATask task)
        {
            try
            {
                var createdTask = await _taskRepo.CreateTaskAsync(task);
                return CreatedAtAction(nameof(AddTask), createdTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = "There was an issue while creating the task. Check log for details."
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(ATask task)
        {
            try
            {
                var taskAvailable = await _taskRepo.GetTaskByIdAsync(task.Id);


                if (taskAvailable != null)
                {
                    taskAvailable.Name = task.Name;
                    taskAvailable.IsComplete = task.IsComplete;

                    await _taskRepo.UpdateTaskAsync(taskAvailable);
                }
                else
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = "Task not found."
                    });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = "There was an issue while updating the task. Check log for details."
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var taskAvailable = await _taskRepo.GetTaskByIdAsync(id);


                if (taskAvailable != null)
                    await _taskRepo.DeleteTaskAsync(taskAvailable);
                else
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = "Task not found."
                    });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = "There was an issue while deleting the task. Check log for details."
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTask()
        {
            try
            {
                var taskList = await _taskRepo.GetAllTaskAsync();
                return Ok(taskList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = "There was an issue while getting all the task. Check log for details."
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            try
            {
                var task = await _taskRepo.GetTaskByIdAsync(id);

                if(task == null)
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = "Task not found."
                    });

                return Ok(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = "There was an issue while getting the task. Check log for details."
                });
            }
        }
    }
}
