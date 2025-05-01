namespace ProjectTaskManagement.API.DTOs;

public class GetTaskDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProjectName { get; set; }
    public string Status { get; set; }
    public DateTime DateCreated { get; set; }
    public string CreatedByUser { get; set; }

}

public class AddTaskDto
{
    public string Name { get; set; }
    public int ProjectId { get; set; }
    public int StatusId { get; set; }
    public int CreatedByUserId { get; set; }

}

//public class UpdateTaskNameDto
//{
//    public string Name { get; set; }


//}

//public class UpdateTaskStatusDto
//{
//    [AllowedValues([Status.Open, Status.Closed, Status.InProgress])]
//    public Status Status { get; set; }
//}
