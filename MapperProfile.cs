using Mapster;

namespace ProjectTaskManagement.API;

public static class MapperProfile
{
    public static void ConfigMapster(this IServiceCollection services)
    {
        TypeAdapterConfig<Task, GetTaskDto>.NewConfig()
            .Map(dest => dest.ProjectName, src => src.Project.Name)
            .Map(dest => dest.Status, src => src.Status.Name)
            .Map(dest => dest.CreatedByUser, src => src.CreatedByUser.FirstName + " " + src.CreatedByUser.LastName);

        TypeAdapterConfig<Comment, GetCommentDto>.NewConfig()
            .Map(dest => dest.UserName, src => src.TaskAssignment.User.FirstName + " " + src.TaskAssignment.User.LastName)
            .Map(dest => dest.TaskName, src => src.TaskAssignment.Task.Name);


        TypeAdapterConfig<TaskAssignment, GetTaskAssignmentDto>.NewConfig()
            .Map(dest => dest.UserName, src => src.User.FirstName + " " + src.User.LastName);


        TypeAdapterConfig<TaskStatusHistory, GetTaskStatusHistoryDto>.NewConfig()
            .Map(dest => dest.ByUser, src => src.TaskAssignment.User.FirstName + " " + src.TaskAssignment.User.LastName)
            .Map(dest => dest.TaskName, src => src.TaskAssignment.Task.Name)
            .Map(dest => dest.ChangedToStatus, src => src.Status.Name);


    }
}
