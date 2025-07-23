using TaskAndTeamManagementSystem.Models;
namespace TaskAndTeamManagementSystem.DTOs
{
    public class GetTeamDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<User> Members { get; set; } = new List<User>();
        public ICollection<UserTask> Tasks { get; set; } = new List<UserTask>();

    }
}
