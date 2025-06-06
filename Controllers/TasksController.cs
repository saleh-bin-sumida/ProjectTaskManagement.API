﻿namespace ProjectTaskManagement.API.Controllers;

[ApiController]

public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<TasksController> _logger;

    public TasksController(AppDbContext context, ILogger<TasksController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all tasks for a specific project.
    /// </summary>
    /// <param name="projectId">The ID of the project.</param>
    /// <param name="pageNumber">The page number for pagination (default is 1).</param>
    /// <param name="pageSize">The page size for pagination (default is 10).</param>
    /// <returns>A paginated list of tasks for the specified project.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet(ApiSystemRouts.Tasks.GetAllByProject)]
    public async Task<IActionResult> GetAllTaskByProject(int projectId, int pageNumber = 1, int pageSize = 10)
    {
        var query = _context.Tasks.Where(x => x.ProjectId == projectId);
        var totalRecords = await query.CountAsync();
        var tasks = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToType<GetTaskDto>()
            .ToListAsync();

        var pagedResult = PagedResult<GetTaskDto>.Create(tasks, totalRecords, pageNumber, pageSize);
        _logger.LogInformation("GetAllTaskByProject executed successfully for ProjectId: {ProjectId}", projectId);
        return Ok(BaseResponse<PagedResult<GetTaskDto>>.SuccessResponse(pagedResult));
    }

    /// <summary>
    /// Retrieves a task by its ID.
    /// </summary>
    /// <param name="id">The ID of the task.</param>
    /// <returns>The task with the specified ID.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet(ApiSystemRouts.Tasks.GetById)]
    public async Task<IActionResult> GetTaskById(int id)
    {
        var task = await _context.Tasks
            .ProjectToType<GetTaskDto>()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (task is null)
        {
            _logger.LogWarning("GetTaskById failed. No task found with Id: {Id}", id);
            return NotFound(BaseResponse<object>.ErrorResponse($"No task found with Id {id}"));
        }

        _logger.LogInformation("GetTaskById executed successfully for Id: {Id}", id);
        return Ok(BaseResponse<GetTaskDto>.SuccessResponse(task));
    }

    /// <summary>
    /// Adds a new task.
    /// </summary>
    /// <param name="taskDto">The task data transfer object.</param>
    /// <returns>The created task.</returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost(ApiSystemRouts.Tasks.Add)]
    public async Task<IActionResult> AddTask(AddTaskDto taskDto)
    {
        if (!await _context.Projects.AnyAsync(x => x.Id == taskDto.ProjectId))
        {
            _logger.LogWarning("AddTask failed. Invalid ProjectId: {ProjectId}", taskDto.ProjectId);
            return BadRequest(BaseResponse<object>.ErrorResponse("Invalid project ID!"));
        }

        var newTask = taskDto.Adapt<Task>();
        await _context.Tasks.AddAsync(newTask);
        await _context.SaveChangesAsync();

        _logger.LogInformation("AddTask executed successfully for TaskId: {TaskId}", newTask.Id);
        return CreatedAtAction(nameof(GetTaskById), new { id = newTask.Id }, BaseResponse<object>.SuccessResponse(null, "Task added successfully"));
    }

    /// <summary>
    /// Updates the name of a task.
    /// </summary>
    /// <param name="id">The ID of the task to update.</param>
    /// <param name="name">The new name of the task.</param>
    /// <returns>No content if successful.</returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut(ApiSystemRouts.Tasks.UpdateName)]
    public async Task<IActionResult> UpdateTaskName(int id, [FromBody] string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            _logger.LogWarning("UpdateTaskName failed. Name is null or empty for Id: {Id}", id);
            return BadRequest(BaseResponse<object>.ErrorResponse("Name cannot be null or empty"));
        }

        var task = await _context.Tasks.FindAsync(id);

        if (task is null)
        {
            _logger.LogWarning("UpdateTaskName failed. No task found with Id: {Id}", id);
            return NotFound(BaseResponse<object>.ErrorResponse($"No task found with Id {id}"));
        }

        task.Name = name;
        await _context.SaveChangesAsync();

        _logger.LogInformation("UpdateTaskName executed successfully for Id: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Updates the status of a task.
    /// </summary>
    /// <param name="id">The ID of the task to update.</param>
    /// <param name="statusId">The new status ID of the task.</param>
    /// <param name="userId">The ID of the user making the update.</param>
    /// <returns>No content if successful.</returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut(ApiSystemRouts.Tasks.UpdateStatus)]
    public async Task<IActionResult> UpdateTaskStatus(int id, int statusId, int userId)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task is null)
        {
            _logger.LogWarning("UpdateTaskStatus failed. No task found with Id: {Id}", id);
            return NotFound(BaseResponse<object>.ErrorResponse($"No task found with Id {id}"));
        }

        task.StatusId = statusId;
        var assignment = await _context.TaskAssignments.SingleOrDefaultAsync(x => x.TaskId == id && x.UserId == userId);
        await _context.AddAsync(new TaskStatusHistory(statusId, assignment.Id));

        await _context.SaveChangesAsync();

        _logger.LogInformation("UpdateTaskStatus executed successfully for TaskId: {TaskId}", id);
        return NoContent();
    }

    /// <summary>
    /// Deletes a task by its ID.
    /// </summary>
    /// <param name="id">The ID of the task to delete.</param>
    /// <returns>A success or error message.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete(ApiSystemRouts.Tasks.Delete)]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var rows = await _context.Tasks.Where(x => x.Id == id).ExecuteDeleteAsync();

        if (rows > 0)
        {
            _logger.LogInformation("DeleteTask executed successfully for Id: {Id}", id);
            return Ok(BaseResponse<object>.SuccessResponse(null, "Task deleted successfully"));
        }

        _logger.LogWarning("DeleteTask failed. No task found with Id: {Id}", id);
        return NotFound(BaseResponse<object>.ErrorResponse($"No task found with Id {id}"));
    }
}
