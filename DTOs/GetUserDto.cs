using TaskAndTeamManagementSystem.Models;
namespace TaskAndTeamManagementSystem.DTOs
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public UserRole? Role { get; set; }
  
        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public ICollection<UserTask> AssignedTasks { get; set; } = new List<UserTask>();

    }
}
