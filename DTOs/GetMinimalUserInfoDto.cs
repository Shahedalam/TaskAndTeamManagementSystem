using TaskAndTeamManagementSystem.Models;
namespace TaskAndTeamManagementSystem.DTOs
{
    public class GetMinimalUserInfoDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public UserRole? Role { get; set; }

    }
}
