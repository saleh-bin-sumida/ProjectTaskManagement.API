using System.Linq.Expressions;

namespace TaskTaskManagement.API.Mapping;

public static class TaskMapper
{
    public static Expression<Func<Task, GetTaskDto>> GetDto()
    {
        return x => new GetTaskDto
        {
            Id = x.Id,
            Name = x.Name,
            ProjectName = x.Project.Name,
            Status = x.Status.Name,
            DateCreated = x.DateCreated,
            CreatedByUser = x.CreatedByUser.FirstName + " " + x.CreatedByUser.FirstName

        };
    }

    public static Task ToTask(this AddTaskDto taskDto)
    {
        return new Task
        {
            Name = taskDto.Name,
            ProjectId = taskDto.ProjectId,
            StatusId = taskDto.StatusId,
            CreatedByUserId = taskDto.CreatedByUserId,
            DateCreated = DateTime.Now
        };
    }

    //public static void UpdateName(this Task task, UpdateTaskNameDto taskDto)
    //{
    //    task.Name = taskDto.Name;
    //    task.Status = taskDto.Status;


    //}

    //public static void UpdateStatusName(this Task task, UpdateTaskNameDto taskDto)
    //{
    //    task.Status = taskDto.Status;
    //}
}
