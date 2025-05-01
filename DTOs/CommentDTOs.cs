namespace ProjectTaskManagement.API.DTOs;

public class GetCommentDto
{
    public int Id { get; set; }
    public string Message { get; set; }
    public string UserName { get; set; }
    public string TaskName { get; set; }
    public DateTime DateCreated { get; set; }

}

public class AddCommentDto
{
    public string Message { get; set; }
    public int TaskAssignmentId { get; set; }
}

public class UpdateCommentDto
{
    public string Message { get; set; }
}
