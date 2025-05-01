using ProjectTaskManagement.API.DTOs;
using ProjectTaskManagement.API.Models;
using System.Linq.Expressions;

namespace CommentTaskManagement.API.Mapping;

public static class CommentMapper
{
    public static Expression<Func<Comment, GetCommentDto>> GetDto()
    {
        return x => new GetCommentDto
        {
            Id = x.Id,
            Message = x.Message,
            UserName = x.TaskAssignment.User.FirstName + " " + x.TaskAssignment.User.LastName,
            TaskName = x.TaskAssignment.Task.Name,
            DateCreated = x.DateCreated
        };
    }


    public static Comment ToComment(this AddCommentDto commentDto)
    {
        return new Comment
        {
            Message = commentDto.Message,
            TaskAssignmentId = commentDto.TaskAssignmentId,
            DateCreated = DateTime.Now
        };
    }

    public static void Update(this Comment comment, UpdateCommentDto commentDto)
    {
        comment.Message = commentDto.Message;
    }
}
