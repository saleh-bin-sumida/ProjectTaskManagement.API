using System.Text.Json.Serialization;

namespace ProjectTaskManagement.API.Models;

public class TaskStatus
{
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonIgnore]
    public ICollection<Task>? Tasks { get; set; }
}
