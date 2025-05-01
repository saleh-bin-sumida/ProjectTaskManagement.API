using ProjectTaskManagement.API.DTOs;
using ProjectTaskManagement.API.Models;
using System.Linq.Expressions;

namespace ProjectTaskManagement.API.Mapping;

public static class ProjectMapper
{
    public static Expression<Func<Project, GetProjectDto>> GetDto()
    {
        return x => new GetProjectDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            DateCreated = x.DateCreated

        };
    }


    public static Project ToProject(this AddProjectDto projectDto)
    {
        return new Project
        {
            Name = projectDto.Name,
            Description = projectDto.Description,
            DateCreated = DateTime.Now
        };
    }

    public static void Update(this Project project, UpdateProjectDto projectDto)
    {
        project.Name = projectDto.Name;
        project.Description = projectDto.Description;
    }
}
