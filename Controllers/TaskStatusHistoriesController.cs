using Mapster;

namespace ProjectTaskManagement.API.Controllers;

[ApiController]
public class TaskStatusHistoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public TaskStatusHistoriesController(AppDbContext context)
    {
        _context = context;
    }

    #region Task Status History Endpoints

    /// <summary>
    /// Retrieves all task status histories with pagination.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paginated list of task status histories.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet(ApiSystemRouts.TaskStatusHistories.GetAll)]
    public async Task<IActionResult> GetAllTasksStatusHistories(int pageNumber = 1, int pageSize = 10)
    {
        var query = _context.TaskStatusHistories;
        var totalRecords = await query.CountAsync();
        var tasksStatusHistories = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToType<GetTaskStatusHistoryDto>()
            .ToListAsync();

        return Ok(tasksStatusHistories);
    }

    ///// <summary>
    ///// Retrieves all status histories for a specific task assignment.
    ///// </summary>
    ///// <param name="taskAssigmentId">The ID of the task assignment.</param>
    ///// <returns>A list of status histories for the specified task assignment.</returns>
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[HttpGet(ApiSystemRouts.TaskStatusHistories.GetByTaskId)]
    //public async Task<IActionResult> GetAllTaskStatusHistories(int taskId)
    //{
    //    var taskStatusHistories = await _context.TaskStatusHistories.Where(x => x.TaskAssignmentId == taskAssigmentId)
    //        .Select(x => new GetTaskStatusHistoryDto
    //        {
    //            Id = x.Id,
    //            TaskName = x.TaskAssignment.Task.Name,
    //            UserName = x.TaskAssignment.User.FirstName,
    //            StatusName = x.Status.Name,
    //            Date = x.Date
    //        }).ToListAsync();


    //    return Ok(taskStatusHistories);
    //}

    #endregion
}
