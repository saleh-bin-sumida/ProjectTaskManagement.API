namespace ProjectTaskManagement.API.Controllers;

[ApiController]
[Route(ApiSystemRouts.TaskStatuses.Base)]
public class TaskStatusesController : ControllerBase
{
    private readonly AppDbContext _context;

    public TaskStatusesController(AppDbContext context)
    {
        _context = context;
    }

    #region Task Status Endpoints

    /// <summary>
    /// Retrieves all task statuses.
    /// </summary>
    /// <returns>A list of all task statuses.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet(ApiSystemRouts.TaskStatuses.GetAll)]
    public async Task<IActionResult> GetAllTaskStatuses()
    {
        var taskStatuses = await _context.TaskStatuses.ToListAsync();
        return Ok(BaseResponse<IEnumerable<TaskStatus>>.SuccessResponse(taskStatuses));
    }

    /// <summary>
    /// Retrieves a task status by its ID.
    /// </summary>
    /// <param name="id">The ID of the task status.</param>
    /// <returns>The task status with the specified ID.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet(ApiSystemRouts.TaskStatuses.GetById)]
    public async Task<IActionResult> GetTaskStatusById(int id)
    {
        var taskStatus = await _context.TaskStatuses.FindAsync(id);

        if (taskStatus is null)
        {
            var errorResponse = BaseResponse<object>.ErrorResponse($"No task status found with Id {id}");
            return NotFound(errorResponse);
        }

        return Ok(BaseResponse<TaskStatus>.SuccessResponse(taskStatus));
    }

    /// <summary>
    /// Creates a new task status.
    /// </summary>
    /// <param name="taskStatus">The task status to create.</param>
    /// <returns>The created task status.</returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost(ApiSystemRouts.TaskStatuses.Add)]
    public async Task<IActionResult> AddTaskStatus(TaskStatus taskStatus)
    {
        if (string.IsNullOrEmpty(taskStatus.Name))
        {
            var errorResponse = BaseResponse<object>.ErrorResponse("Task status name cannot be null or empty");
            return BadRequest(errorResponse);
        }

        await _context.TaskStatuses.AddAsync(taskStatus);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTaskStatusById), new { id = taskStatus.Id }, BaseResponse<object>.SuccessResponse(null, "Task status created successfully"));
    }

    /// <summary>
    /// Updates an existing task status.
    /// </summary>
    /// <param name="id">The ID of the task status to update.</param>
    /// <param name="taskStatus">The updated task status data.</param>
    /// <returns>No content if successful.</returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut(ApiSystemRouts.TaskStatuses.Update)]
    public async Task<IActionResult> UpdateTaskStatus(int id, TaskStatus taskStatus)
    {
        if (id != taskStatus.Id)
        {
            var errorResponse = BaseResponse<object>.ErrorResponse("Task status ID mismatch");
            return BadRequest(errorResponse);
        }

        var existingTaskStatus = await _context.TaskStatuses.FindAsync(id);

        if (existingTaskStatus is null)
        {
            var errorResponse = BaseResponse<object>.ErrorResponse($"No task status found with Id {id}");
            return NotFound(errorResponse);
        }

        existingTaskStatus.Name = taskStatus.Name;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Deletes a task status by its ID.
    /// </summary>
    /// <param name="id">The ID of the task status to delete.</param>
    /// <returns>A success or error message.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete(ApiSystemRouts.TaskStatuses.Delete)]
    public async Task<IActionResult> DeleteTaskStatus(int id)
    {
        var taskStatus = await _context.TaskStatuses.FindAsync(id);

        if (taskStatus is null)
        {
            var errorResponse = BaseResponse<object>.ErrorResponse($"No task status found with Id {id}");
            return NotFound(errorResponse);
        }

        _context.TaskStatuses.Remove(taskStatus);
        await _context.SaveChangesAsync();

        return Ok(BaseResponse<object>.SuccessResponse(null, "Task status deleted successfully"));
    }

    #endregion
}
