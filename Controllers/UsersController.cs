namespace UserTaskManagement.API.Controllers;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<UsersController> _logger;

    public UsersController(AppDbContext context, ILogger<UsersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    #region User Endpoints

    /// <summary>
    /// Retrieves all users with optional search and pagination.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="searchTerm">The search term to filter users.</param>
    /// <returns>A paginated list of users.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet(ApiSystemRouts.Users.GetAll)]
    public async Task<IActionResult> GetAllUsers(int pageNumber, int pageSize, string? searchTerm)
    {
        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(u => u.FirstName.Contains(searchTerm)
                || u.LastName.Contains(searchTerm)
                || u.Email.Contains(searchTerm));
        }

        var totalRecords = await query.CountAsync();

        var users = await query
            .ProjectToType<GetUserDto>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        _logger.LogInformation("Retrieved {UserCount} users for page {PageNumber} with page size {PageSize}.", users.Count, pageNumber, pageSize);

        var pagedResult = PagedResult<GetUserDto>.Create(users, totalRecords, pageNumber, pageSize);

        var response = BaseResponse<PagedResult<GetUserDto>>.SuccessResponse(pagedResult);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The user with the specified ID.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet(ApiSystemRouts.Users.GetById)]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _context.Users
            .ProjectToType<GetUserDto>()
            .SingleOrDefaultAsync(x => x.Id == id);

        if (user is null)
        {
            _logger.LogWarning("No user found with Id {UserId}.", id);
            var errorResponse = BaseResponse<object>.ErrorResponse($"No user found with Id {id}");
            return NotFound(errorResponse);
        }

        _logger.LogInformation("Retrieved user with Id {UserId}.", id);
        var response = BaseResponse<GetUserDto>.SuccessResponse(null);
        return Ok(response);
    }

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="userDto">The user data transfer object.</param>
    /// <returns>The created user.</returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost(ApiSystemRouts.Users.Add)]
    public async Task<IActionResult> AddUser(AddUserDto userDto)
    {
        var newUser = userDto.Adapt<User>();
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Added new user with Id {UserId}.", newUser.Id);

        var response = BaseResponse<object>.SuccessResponse(null, "User added successfully");
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, response);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="userDto">The updated user data.</param>
    /// <returns>A success or error message.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut(ApiSystemRouts.Users.Update)]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDto userDto)
    {
        var user = await _context.Users.FindAsync(id);

        if (user is null)
        {
            _logger.LogWarning("No user found with Id {UserId} for update.", id);
            var errorResponse = BaseResponse<object>.ErrorResponse($"No user found with Id {id}");
            return NotFound(errorResponse);
        }

        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Updated user with Id {UserId}.", id);

        var response = BaseResponse<object>.SuccessResponse(null, "User updated successfully");
        return Ok(response);
    }

    /// <summary>
    /// Deletes a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <returns>A success or error message.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete(ApiSystemRouts.Users.Delete)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var rows = await _context.Users.Where(x => x.Id == id).ExecuteDeleteAsync();

        if (rows > 0)
        {
            _logger.LogInformation("Deleted user with Id {UserId}.", id);
            var response = BaseResponse<object>.SuccessResponse(null, "User deleted successfully");
            return Ok(response);
        }

        _logger.LogWarning("No user found with Id {UserId} for deletion.", id);
        var errorResponse = BaseResponse<object>.ErrorResponse($"No user found with Id {id}");
        return NotFound(errorResponse);
    }

    #endregion
}
