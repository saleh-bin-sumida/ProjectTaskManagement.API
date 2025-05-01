namespace ProjectTaskManagement.API.Models;

public class Project
{
    #region Keys
    public int Id { get; set; }
    #endregion

    #region Scalar Properties
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateCreated { get; set; }
    #endregion

    #region Collections
    public ICollection<Task> Tasks = [];
    #endregion

}
