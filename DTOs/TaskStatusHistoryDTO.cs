namespace ProjectTaskManagement.API.DTOs;

public class GetTaskStatusHistoryDto
{
    public int Id { get; set; }
    public string TaskName { get; set; }
    public string ChangedToStatus { get; set; }
    public string ByUser { get; set; }
    public DateTime Date { get; set; }
}
