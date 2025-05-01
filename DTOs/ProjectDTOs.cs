namespace ProjectTaskManagement.API.DTOs;

public class GetProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    //public string Description { get; set; }
    //public DateTime DateCreated { get; set; }

}

public class AddProjectDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class UpdateProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
