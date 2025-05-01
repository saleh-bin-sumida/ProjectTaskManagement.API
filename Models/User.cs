namespace ProjectTaskManagement.API.Models;

public class User
{
    #region Keys
    public int Id { get; set; }
    #endregion

    #region Scalar Properties
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateCreated { get; set; }
    #endregion

    #region Collections
    public ICollection<Task>? TasksCreated = [];
    public ICollection<TaskAssignment> TaskAssignments = [];
    #endregion
}
