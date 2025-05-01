namespace ProjectTaskManagement.API.DTOs;

public class GetTaskAssignmentDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string UserName { get; set; }
    public string TaskName { get; set; }
}

public class AddTaskAssignmentDto
{
    public int UserId { get; set; }
    public int TaskId { get; set; }
}

