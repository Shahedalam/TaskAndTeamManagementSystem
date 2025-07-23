using System.Text.Json.Serialization;
namespace TaskAndTeamManagementSystem.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole { Admin, Manager, Employee }
    public enum TaskStatus { Todo, InProgress, Done }

    public class User
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public required string Email { get; set; }
        public string? Password { get; set; }
        public UserRole Role { get; set; }
        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public ICollection<UserTask> AssignedTasks { get; set; } = new List<UserTask>();
    }

    public class Team
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<User> Members { get; set; } = new List<User>();
        public ICollection<UserTask> Tasks { get; set; } = new List<UserTask>();
    }

    public class UserTask
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public required TaskStatus Status { get; set; }
        public required int AssignedToUserId { get; set; }
        public required int CreatedByUserId { get; set; }
        public required int TeamId { get; set; }
        public required DateTime DueDate { get; set; }
        public required User AssignedToUser { get; set; }
        public required User CreatedByUser { get; set; }
        public required Team Team { get; set; }
    }
}