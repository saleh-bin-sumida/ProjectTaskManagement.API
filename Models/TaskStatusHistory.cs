namespace ProjectTaskManagement.API.Models;

public class TaskStatusHistory
{
    #region Keys
    public int Id { get; set; }
    public int StatusId { get; set; }
    public int TaskAssignmentId { get; set; }
    #endregion

    #region Navigations
    public TaskStatus Status { get; set; }
    public TaskAssignment TaskAssignment { get; set; }
    #endregion

    public DateTime Date { get; set; }

    public TaskStatusHistory(int statusId, int taskAssignmentId)
    {
        this.StatusId = statusId;
        this.TaskAssignmentId = taskAssignmentId;
    }
}
