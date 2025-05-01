namespace CommentTaskManagement.API.Controllers;

[ApiController]
public class CommentsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<CommentsController> _logger;

    public CommentsController(AppDbContext context, ILogger<CommentsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    #region Comment Endpoints

    /// <summary>
    /// Retrieves all comments for a specific task assignment with pagination.
    /// </summary>
    /// <param name="taskAssigmentId">The ID of the task assignment.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paginated list of comments.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet(ApiSystemRouts.Comments.GetAll)]
    public async Task<IActionResult> GetAllCommentsByTask(int taskAssigmentId, int pageNumber = 1, int pageSize = 10)
    {
        var query = _context.Comments.Where(x => x.TaskAssignmentId == taskAssigmentId);
        var totalRecords = await query.CountAsync();
        _logger.LogInformation("Total comments retrieved for TaskAssignmentId {TaskAssignmentId}: {TotalRecords}", taskAssigmentId, totalRecords);

        var comments = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToType<GetCommentDto>()
            .ToListAsync();

        var pagedResult = PagedResult<GetCommentDto>.Create(comments, totalRecords, pageNumber, pageSize);
        return Ok(BaseResponse<PagedResult<GetCommentDto>>.SuccessResponse(pagedResult));
    }

    /// <summary>
    /// Retrieves a comment by its ID.
    /// </summary>
    /// <param name="id">The ID of the comment.</param>
    /// <returns>The comment with the specified ID.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet(ApiSystemRouts.Comments.GetById)]
    public async Task<IActionResult> GetCommentById(int id)
    {
        var comment = await _context.Comments
            .ProjectToType<GetCommentDto>()
            .FirstOrDefaultAsync(x => x.Id == id);


        if (comment is null)
            return NotFound($"no comment with Id {id}");

        return Ok(comment);
    }

    /// <summary>
    /// Adds a new comment.
    /// </summary>
    /// <param name="commentDto">The comment data transfer object.</param>
    /// <returns>The created comment.</returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost(ApiSystemRouts.Comments.Add)]
    public async Task<IActionResult> AddComment(AddCommentDto commentDto)
    {
        if (!await _context.TaskAssignments.AnyAsync(x => x.Id == commentDto.TaskAssignmentId))
            return BadRequest("invalid comment!");



        var newComment = commentDto.Adapt<Comment>();
        await _context.Comments.AddAsync(newComment);
        await _context.SaveChangesAsync();

        return Created();
    }

    /// <summary>
    /// Updates an existing comment.
    /// </summary>
    /// <param name="id">The ID of the comment to update.</param>
    /// <param name="commentDto">The updated comment data.</param>
    /// <returns>No content if successful.</returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut(ApiSystemRouts.Comments.Update)]
    public async Task<IActionResult> UpdateComment(int id, UpdateCommentDto commentDto)
    {
        var comment = await _context.Comments.FindAsync(id);

        if (comment is null)
            return NotFound($"no comment with Id {id}");

        comment.Message = commentDto.Message;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Deletes a comment by its ID.
    /// </summary>
    /// <param name="id">The ID of the comment to delete.</param>
    /// <returns>A success or error message.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete(ApiSystemRouts.Comments.Delete)]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var rows = await _context.Comments.Where(x => x.Id == id).ExecuteDeleteAsync();

        if (rows > 0)
            return Ok("Comment Deleted Succfuly");

        return BadRequest($"no comment with Id {id}");
    }

    #endregion
}
