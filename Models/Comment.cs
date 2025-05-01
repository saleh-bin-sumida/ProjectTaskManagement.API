namespace ProjectTaskManagement.API.Models;

public class Comment
{
    #region Keys
    public int Id { get; set; }
    public int TaskAssignmentId { get; set; }
    #endregion

    #region Scalar Properties
    public string Message { get; set; }
    public DateTime DateCreated { get; set; }
    #endregion

    #region Navigations
    public TaskAssignment TaskAssignment { get; set; }
    #endregion
}
