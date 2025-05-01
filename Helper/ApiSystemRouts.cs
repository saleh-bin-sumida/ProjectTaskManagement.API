namespace ProjectTaskManagement.API.Helper;

public static class ApiSystemRouts
{
    public static class Projects
    {
        public const string Base = "api/projects";
        public const string GetAll = Base;
        public const string GetById = Base + "/{id}";
        public const string Add = Base;
        public const string Update = Base + "/{id}";
        public const string Delete = Base + "/{id}";
    }
    public static class Tasks
    {
        public const string Base = "api/tasks";
        public const string GetAll = Base;
        public const string GetAllByProject = Base + "getAllByProject/{projectId}";
        public const string GetById = Base + "/{id}";
        public const string Add = Base;
        public const string UpdateName = Base + "/updateName/{id}";
        public const string UpdateStatus = Base + "/updateStatus/{id}/{status}";
        public const string Delete = Base + "/{id}";
    }
    public static class Users
    {
        public const string Base = "api/users";
        public const string GetAll = Base;
        public const string GetById = Base + "/{id}";
        public const string Add = Base;
        public const string Update = Base + "/{id}";
        public const string Delete = Base + "/{id}";
    }
    public static class TaskAssignments
    {
        public const string Base = "api/taskAssignments";
        public const string GetAll = Base;
        public const string GetById = Base + "/{id}";
        public const string GetByTaskId = Base + "/ByTask/{taskId}";
        public const string GetByUserId = Base + "/ByUser/{userId}";
        public const string AssignUserToTask = Base + "/assignUserToTask";
        public const string Update = Base + "/{id}";
        public const string Delete = Base + "/{id}";
    }
    public static class Comments
    {
        public const string Base = "api/comments";
        public const string GetAll = Base;
        public const string GetAllByTask = Base + "/ByTask/{taskId}";
        public const string GetById = Base + "/{id}";
        public const string Add = Base;
        public const string Update = Base + "/{id}";
        public const string Delete = Base + "/{id}";
    }

    public static class TaskStatusHistories
    {
        public const string Base = "api/taskStatusHistories";
        public const string GetAll = Base;
        public const string GetByTaskId = Base + "/{taskId}";

    }

    public static class TaskStatuses
    {
        public const string Base = "api/taskStatuses";
        public const string GetAll = Base;
        public const string GetById = Base + "/{id}";
        public const string Add = Base;
        public const string Update = Base + "/{id}";
        public const string Delete = Base + "/{id}";
    }

}
