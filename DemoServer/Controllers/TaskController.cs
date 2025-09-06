using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Models;
using Interfaces;

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

    public TaskController(IDatabase<TaskEntity> database, IEntityValidation<TaskEntity> validation)
    {
        this.database = database;
        this.validation = validation;
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
        return deletionResult ? Ok() : BadRequest($"No Task with key:{key} found");
    }
}