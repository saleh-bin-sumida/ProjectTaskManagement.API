namespace ProjectTaskManagement.API.Models;

public class TaskAssignment
{
    #region Keys
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TaskId { get; set; }
    #endregion

    #region Scalar Properties
    public DateTime Date { get; init; }
    #endregion

    #region Navigations
    public User User { get; set; }
    public Task Task { get; set; }
    #endregion

    #region Collections
    public ICollection<Comment> Comments = [];
    public ICollection<TaskStatusHistory> TaskStatusHistories = [];
    #endregion
}
