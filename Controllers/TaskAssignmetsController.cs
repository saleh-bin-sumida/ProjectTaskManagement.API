namespace TaskAssignmentTaskManagement.API.Controllers;

[ApiController]
public class TaskAssignmetsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TaskAssignmetsController(AppDbContext context)
    {
        _context = context;
    }

    #region Task Assignment Endpoints

    /// <summary>
    /// Retrieves all task assignments with pagination.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paginated list of task assignments.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet(ApiSystemRouts.TaskAssignments.GetAll)]
    public async Task<IActionResult> GetAllTaskAssignment(int pageNumber = 1, int pageSize = 10)
    {
        var query = _context.TaskAssignments;
        var totalRecords = await query.CountAsync();
        var taskAssignments = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToType<GetTaskAssignmentDto>()

            .ToListAsync();

        var pagedResult = PagedResult<GetTaskAssignmentDto>.Create(taskAssignments, totalRecords, pageNumber, pageSize);
        return Ok(BaseResponse<PagedResult<GetTaskAssignmentDto>>.SuccessResponse(pagedResult));
    }

    /// <summary>
    /// Retrieves all task assignments for a specific task.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paginated list of task assignments.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet(ApiSystemRouts.TaskAssignments.GetByTaskId)]
    public async Task<IActionResult> GetAllTaskAssignmentsByTaskId(int taskId, int pageNumber = 1, int pageSize = 10)
    {
        var query = _context.TaskAssignments.Where(x => x.TaskId == taskId);
        var totalRecords = await query.CountAsync();
        var taskAssignments = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToType<GetTaskAssignmentDto>()
            .ToListAsync();

        var pagedResult = PagedResult<GetTaskAssignmentDto>.Create(taskAssignments, totalRecords, pageNumber, pageSize);
        return Ok(BaseResponse<PagedResult<GetTaskAssignmentDto>>.SuccessResponse(pagedResult));
    }

    /// <summary>
    /// Retrieves all task assignments for a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param="pageSize">The number of items per page.</param>
    /// <returns>A paginated list of task assignments.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet(ApiSystemRouts.TaskAssignments.GetByUserId)]
    public async Task<IActionResult> GetAllTaskAssignmentsByUserId(int userId, int pageNumber = 1, int pageSize = 10)
    {
        var query = _context.TaskAssignments.Where(x => x.UserId == userId);
        var totalRecords = await query.CountAsync();
        var taskAssignments = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToType<GetTaskAssignmentDto>()
            .ToListAsync();

        var pagedResult = PagedResult<GetTaskAssignmentDto>.Create(taskAssignments, totalRecords, pageNumber, pageSize);
        return Ok(BaseResponse<PagedResult<GetTaskAssignmentDto>>.SuccessResponse(pagedResult));
    }

    /// <summary>
    /// Retrieves a task assignment by its ID.
    /// </summary>
    /// <param name="id">The ID of the task assignment.</param>
    /// <returns>The task assignment with the specified ID.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet(ApiSystemRouts.TaskAssignments.GetById)]
    public async Task<IActionResult> GetTaskAssignmentById(int id)
    {
        var taskassignment = await _context.TaskAssignments
            .ProjectToType<GetTaskAssignmentDto>()
            .FirstOrDefaultAsync(x => x.Id == id);


        if (taskassignment is null)
            return NotFound($"no task assignment with Id {id}");

        return Ok(taskassignment);
    }

    /// <summary>
    /// Assigns a user to a task.
    /// </summary>
    /// <param name="taskassignmentDto">The task assignment data transfer object.</param>
    /// <returns>The created task assignment.</returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost(ApiSystemRouts.TaskAssignments.AssignUserToTask)]
    public async Task<IActionResult> AssignUserToTask(AddTaskAssignmentDto taskassignmentDto)
    {

        if (!await _context.Users.AnyAsync(x => x.Id == taskassignmentDto.UserId))
            return BadRequest("invalid user id!");

        if (!await _context.Tasks.AnyAsync(x => x.Id == taskassignmentDto.TaskId))
            return BadRequest("invalid task id!");


        var newTaskAssignment = taskassignmentDto.Adapt<TaskAssignment>();

        await _context.TaskAssignments.AddAsync(newTaskAssignment);
        await _context.SaveChangesAsync();

        return Created();
    }

    #endregion
}
