using ProjectTaskManagement.API.DTOs;
using ProjectTaskManagement.API.Models;
using System.Linq.Expressions;

namespace ProjectTaskAssignmentManagement.API.Mapping;

public static class TaskAssignmentMapper
{
    public static Expression<Func<TaskAssignment, GetTaskAssignmentDto>> GetDto()
    {
        return x => new GetTaskAssignmentDto
        {
            Id = x.Id,
            UserName = x.User.FirstName + " " + x.User.LastName,
            TaskName = x.Task.Name,
            Date = x.Date,
        };
    }

    public static TaskAssignment ToTaskAssignment(this AddTaskAssignmentDto TaskAssignmentDto)
    {
        return new TaskAssignment
        {
            UserId = TaskAssignmentDto.UserId,
            TaskId = TaskAssignmentDto.TaskId,
            Date = DateTime.Now
        };
    }

    //public static void Update(this TaskAssignment TaskAssignment, UpdateTaskAssignmentDto TaskAssignmentDto)
    //{

    //}
}
