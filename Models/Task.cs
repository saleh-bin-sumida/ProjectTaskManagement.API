namespace ProjectTaskManagement.API.Models;

public class Task
{
    #region Keys
    public int Id { get; set; }
    public int? StatusId { get; set; }
    public int ProjectId { get; set; }
    public int? CreatedByUserId { get; set; }
    #endregion

    #region Scalar Properties
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateCreated { get; set; }
    #endregion

    #region Navigations
    public User CreatedByUser { get; set; }
    public TaskStatus Status { get; set; }
    public Project Project { get; set; }
    #endregion

    #region Collections
    public ICollection<TaskAssignment> TaskAssignments = [];
    #endregion
}
