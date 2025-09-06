using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Models;
using Interfaces;
using Microsoft.Extensions.Logging;

namespace DemoServer.Controllers;

/// <summary>
/// Controller for managing tasks in the system
/// </summary>
[Route("[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly IDatabase<TaskEntity> database;
    private readonly IEntityValidation<TaskEntity> validation;
    private readonly object lockObject = new object();
    // Add structured logging
    private readonly ILogger<TaskController> _logger;

    public TaskController(IDatabase<TaskEntity> database, IEntityValidation<TaskEntity> validation, ILogger<TaskController> logger)
    {
        this.database = database;
        this.validation = validation;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all tasks from the database
    /// </summary>
    /// <returns>An array of all tasks in the system</returns>
    /// <response code="200">Returns the list of tasks</response>
    /// <response code="204">If there are no tasks</response>
    [HttpGet]
    [ProducesResponseType(typeof(TaskEntity[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<TaskEntity[]> Get()
    {
        var tasks = database.GetValues().ToArray();
        if (tasks.Length == 0)
        {
            return NoContent();
        }
        return tasks;
    }

    /// <summary>
    /// Creates or updates a task in the database
    /// </summary>
    /// <param name="entity">The task entity to create or update</param>
    /// <returns>OK if successful, BadRequest if validation fails</returns>
    /// <response code="200">Task was successfully created or updated</response>
    /// <response code="400">If the task validation failed</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Post([FromBody] TaskEntity entity)
    {
        lock (lockObject)
        {
            var validationResult = validation.IsValid(database, entity);
            if (validationResult.isValid)
            {
                database.AddOrUpdate(entity);
                // Log important operations
                _logger.LogInformation("Task {TaskId} created", entity.Key);
                return Ok();
            }
            else
            {
                return BadRequest(validationResult.errorMessage);
            }
        }
    }

    /// <summary>
    /// Deletes a task by its unique identifier
    /// </summary>
    /// <param name="key">The unique identifier of the task to delete</param>
    /// <returns>OK if successful, BadRequest if task not found</returns>
    /// <response code="200">Task was successfully deleted</response>
    /// <response code="400">If the task was not found</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Delete(Guid key)
    {
        var deletionResult = database.Delete(key);
        if (deletionResult)
        {
            _logger.LogInformation("Task {TaskId} deleted successfully", key);
            return Ok();
        }
        return BadRequest($"No Task with key:{key} found");
    }
}