using System.ComponentModel.DataAnnotations;

namespace ProjectTaskManagement.API.DTOs;

public class GetUserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateCreated { get; set; }

}


public class AddUserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [EmailAddress(ErrorMessage = "invalid email address")]
    public string Email { get; set; }
}


public class UpdateUserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}


