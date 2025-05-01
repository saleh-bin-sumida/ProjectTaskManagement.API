namespace ProjectTaskManagement.API.Controllers;

[ApiController]
public class ProjectsController(AppDbContext _context) : ControllerBase
{

    #region Project Endpoints

    /// <summary>
    /// Retrieves all projects.
    /// </summary>
    /// <returns>A list of all projects.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet(ApiSystemRouts.Projects.GetAll)]
    public async Task<IActionResult> GetAllProject()
    {
        var projects = await _context.Projects
            .Select(ProjectMapper.GetDto())
        .ToListAsync();

        var response = BaseResponse<IEnumerable<GetProjectDto>>.SuccessResponse(projects);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves a project by its ID.
    /// </summary>
    /// <param name="id">The ID of the project.</param>
    /// <returns>The project with the specified ID.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet(ApiSystemRouts.Projects.GetById)]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var project = await _context.Projects
            .Select(ProjectMapper.GetDto())
            .SingleOrDefaultAsync(x => x.Id == id);

        if (project is null)
            return NotFound($"no project with Id {id}");

        return Ok(project);
    }

    /// <summary>
    /// Adds a new project.
    /// </summary>
    /// <param name="projectDto">The project data transfer object.</param>
    /// <returns>The created project.</returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost(ApiSystemRouts.Projects.Add)]
    public async Task<IActionResult> AddProject(AddProjectDto projectDto)
    {
        var newProject = projectDto.ToProject();
        await _context.Projects.AddAsync(newProject);
        await _context.SaveChangesAsync();

        return Created();
    }

    /// <summary>
    /// Updates an existing project.
    /// </summary>
    /// <param name="id">The ID of the project to update.</param>
    /// <param name="projectDto">The updated project data.</param>
    /// <returns>No content if successful.</returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut(ApiSystemRouts.Projects.Update)]
    public async Task<IActionResult> UpdateProject(int id, UpdateProjectDto projectDto)
    {
        if (id != projectDto.Id)
            return BadRequest("invalid Id");

        var project = await _context.Projects.FindAsync(id);

        if (project is null)
            return NotFound($"no project with Id {id}");

        project.Update(projectDto);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Deletes a project by its ID.
    /// </summary>
    /// <param name="id">The ID of the project to delete.</param>
    /// <returns>A success or error message.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete(ApiSystemRouts.Projects.Delete)]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var rows = await _context.Projects.Where(x => x.Id == id).ExecuteDeleteAsync();

        if (rows > 0)
            return Ok("Project Deleted Succfuly");

        return NotFound($"no project with Id {id}");
    }

    #endregion
}
