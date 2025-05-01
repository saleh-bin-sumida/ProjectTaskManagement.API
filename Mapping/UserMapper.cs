using ProjectTaskManagement.API.DTOs;
using ProjectTaskManagement.API.Models;
using System.Linq.Expressions;

namespace ProjectTaskManagement.API.Mapping;

public static class UserMapper
{
    public static Expression<Func<User, GetUserDto>> GetDto()
    {
        return x => new GetUserDto
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            DateCreated = x.DateCreated
        };
    }


    public static User ToUser(this AddUserDto userDto)
    {
        return new User
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            DateCreated = DateTime.Now
        };
    }

    public static void Update(this User user, UpdateUserDto userDto)
    {
        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;

    }
}
