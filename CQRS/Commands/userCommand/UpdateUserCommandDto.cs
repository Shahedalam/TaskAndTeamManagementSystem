using TaskAndTeamManagementSystem.Models;

namespace TaskAndTeamManagementSystem.CQRS.Commands.userCommand
{
    public class UpdateUserCommandDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public UserRole? Role { get; set; }
        public string? Password { get; set; }
    }
}
